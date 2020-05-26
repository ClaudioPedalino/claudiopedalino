using MediatR;
using System;
using System.Collections.Generic;
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

        public BuyRequestHandler(IOperationRepository operationRepository, IClientRepository clientRepository)
        {
            this.operationRepository = operationRepository ?? throw new ArgumentNullException(nameof(operationRepository));
            this.clientRepository = clientRepository;
        }

        public async Task<ChangeModelResponse> Handle(BuyRequest request, CancellationToken cancellationToken)
        {
            // Validate Client
            //var client = await clientRepository.FindByIdAsync(request.ClientId);
            //if (string.IsNullOrEmpty(client)) throw new ClientIdNotExists();

            //Validate Debit or Credit ExactlyPayment
            if (PayBackWithCardError(request))
                throw new Exception("Si se pagó con tarjeta no debería haber vuelto");

            //01.Create operation to register
            var change = new Change(Guid.NewGuid(), 0);
            var operation = new Operation()
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


            //02.If need payback, create de changeObj and add to operation change
            var responseModel = new ChangeModelResponse();
            if (NeedPayback(request))
            {
                decimal paybackAmount = request.ClientPaymentAmount - request.TotalAmount;
                change.AmountToReturn = paybackAmount;

                responseModel.Amount = paybackAmount;
                responseModel.Bills = GetBills(ref paybackAmount, change);
                responseModel.Coins = GetCoins(ref paybackAmount, change);

                operation.Change = change;
            }

            await operationRepository.Add(operation);

            return responseModel;
        }

        private static bool PayBackWithCardError(BuyRequest request)
            => request.TotalAmount < request.ClientPaymentAmount && (request.PaymentType == 2 || request.PaymentType == 3);

        private static bool NeedPayback(BuyRequest request)
            => request.TotalAmount < request.ClientPaymentAmount
                && request.PaymentType == 1;

        private static List<CoinModelResponse> GetCoins(ref decimal paybackAmount, Change change)
        {
            var coins = new List<CoinModelResponse>();

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
            var bills = new List<BillModelResponse>();

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
