using MediatR;
using Serilog;
using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using TotvsChallengePoC.Core.Request.Buy.Model;
using TotvsChallengePoC.Data.Contracts;
using TotvsChallengePoC.Data.Repositories;
using TotvsChallengePoC.Entities;

namespace TotvsChallengePoC.Core.Request.Buy
{
    public class BuyRequestHandler : IRequestHandler<BuyRequest, ChangeModelResponse>
    {
        private readonly IOperationRepository operationRepository;
        private readonly IClientRepository clientRepository;
        private readonly ILogger logService;

        public BuyRequestHandler(IOperationRepository operationRepository, IClientRepository clientRepository, ILogger logService)
        {
            this.operationRepository = operationRepository ?? throw new ArgumentNullException(nameof(operationRepository));
            this.clientRepository = clientRepository ?? throw new ArgumentNullException(nameof(clientRepository));
            this.logService = logService ?? throw new ArgumentNullException(nameof(logService));
        }

        public async Task<ChangeModelResponse> Handle(BuyRequest request, CancellationToken cancellationToken)
        {
            ChangeModelResponse responseModel = new ChangeModelResponse();
            try
            {
                //00.Validate Existing client
                await ValidateClient(request);

                //01.If payment type is card amount must be exactly
                ValidateDebitOrCreditPayment(request);

                //02.Create operation to register
                Change change = new Change(Guid.NewGuid(), 0);
                Operation operation = CreateOperation(request, change);

                //03.If need payback, create de changeObj and add to operation change
                if (NeedPayback(request))
                    CalculateChangeToReturn(request, change, operation, responseModel);

                await operationRepository.Add(operation);

            }
            catch (Exception ex)
            {
                logService.Error(ex, "Operation failed", request);
            }
            return responseModel;
        }

        private static void CalculateChangeToReturn(BuyRequest request, Change change, Operation operation, ChangeModelResponse responseModel)
        {
            decimal paybackAmount = request.ClientPaymentAmount - request.TotalAmount;
            change.AmountToReturn = paybackAmount;

            responseModel.Amount = paybackAmount;
            responseModel.Bills = GetBills(ref paybackAmount, change);
            responseModel.Coins = GetCoins(ref paybackAmount, change);

            operation.Change = change;
        }

        private static Operation CreateOperation(BuyRequest request, Change change)
           => new Operation()
           {
               Id = new Guid(),
               DateCreated = System.DateTimeOffset.Now,
               TotalAmount = request.TotalAmount,
               ClientPaymentAmount = request.ClientPaymentAmount,
               PaymentTypeId = request.PaymentType,
               ClientId = new Guid(request.ClientId),
               ChangeId = Guid.NewGuid(),
               Change = change
           };

        private static void ValidateDebitOrCreditPayment(BuyRequest request)
        {
            if (PayBackWithCardError(request))
                throw new Exception("Si se pagó con tarjeta no debería haber vuelto");
        }

        private async Task ValidateClient(BuyRequest request)
        {
            Client client = await clientRepository.FindClientByIdAsync(request.ClientId);
            if (client == null) throw new Exception("El cliente no existe en la base de datos");
        }

        private static bool PayBackWithCardError(BuyRequest request)
            => request.TotalAmount < request.ClientPaymentAmount && (request.PaymentType == 2 || request.PaymentType == 3);

        private static bool NeedPayback(BuyRequest request)
            => request.TotalAmount < request.ClientPaymentAmount
                && request.PaymentType == 1;

        private static List<CoinModelResponse> GetCoins(ref decimal paybackAmount, Change change)
        {
            List<CoinModelResponse> coins = new List<CoinModelResponse>();

            foreach (var c in MoneyHelper.Coins)
            {
                if (paybackAmount >= c.Value)
                {
                    var coin = new CoinModelResponse((int)(paybackAmount / c.Value), c.Key);
                    paybackAmount = paybackAmount - coin.Quantity * c.Value;
                    SetCoinValue(change, c, coin);
                    coins.Add(coin);
                }
            }

            return coins;
        }

        private static List<BillModelResponse> GetBills(ref decimal paybackAmount, Change change)
        {
            List<BillModelResponse> bills = new List<BillModelResponse>();

            foreach (var b in MoneyHelper.Bills)
            {
                if (paybackAmount >= b.Value)
                {
                    var bill = new BillModelResponse((int)paybackAmount / b.Value, b.Key);
                    paybackAmount = paybackAmount - (bill.Quantity * b.Value);
                    SetBillValue(change, b, bill);
                    bills.Add(bill);
                }
            }

            return bills;
        }


        //NOTE: I couldnt make this two by generics, but almost use reflection
        private static void SetBillValue(Change change, KeyValuePair<string, int> item, BillModelResponse bill)
        {
            //https://stackoverflow.com/questions/619767/set-object-property-using-reflection
            PropertyInfo prop = change.GetType().GetProperty(item.Key, BindingFlags.Public | BindingFlags.Instance);
            if (null != prop && prop.CanWrite)
                prop.SetValue(change, bill.Quantity, null);
        }

        private static void SetCoinValue(Change change, KeyValuePair<string, decimal> item, CoinModelResponse coin)
        {
            PropertyInfo prop = change.GetType().GetProperty(item.Key, BindingFlags.Public | BindingFlags.Instance);
            if (null != prop && prop.CanWrite)
                prop.SetValue(change, coin.Quantity, null);
        }

    }
}
