using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TotvsChallenge.Data.Contracts;
using TotvsChallenge.Data.Models;
using TotvsChallenge.Entities;

namespace TotvsChallenge.Core.Requests.CalculateChange
{
    public class CalculateChangeRequestHandler : IRequestHandler<CalculateChangeRequest, ChangeModelResponse>
    {
        private readonly IOperationRepository operationRepository;

        public CalculateChangeRequestHandler(IOperationRepository operationRepository)
        {
            this.operationRepository = operationRepository ?? throw new ArgumentNullException(nameof(operationRepository));
        }

        public async Task<ChangeModelResponse> Handle(CalculateChangeRequest request, CancellationToken cancellationToken)
        {
            //TODO: Validate client exists

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

        private static bool PayBackWithCardError(CalculateChangeRequest request)
        {
            return request.TotalAmount < request.ClientPaymentAmount && (request.PaymentType == 2 || request.PaymentType == 3);
        }

        private static bool NeedPayback(CalculateChangeRequest request)
            => request.TotalAmount < request.ClientPaymentAmount
                && request.PaymentType == 1;

        private static List<CoinModelResponse> GetCoins(ref decimal paybackAmount, Change change)
        {
            var coins = new List<CoinModelResponse>();
            if ((paybackAmount >= (decimal)0.5))
            {
                var coin = new CoinModelResponse((int)(paybackAmount / (decimal)0.5), "C50");
                paybackAmount = paybackAmount - (decimal)(coin.Quantity * 0.5);
                change.C50 = coin.Quantity;
                coins.Add(coin);
            }
            if ((paybackAmount >= (decimal)0.1))
            {
                var coin = new CoinModelResponse(Convert.ToInt32(paybackAmount / (decimal)0.1), "C10");
                paybackAmount = paybackAmount - (decimal)(coin.Quantity * 0.1);
                change.C10 = coin.Quantity;
                coins.Add(coin);
            }
            if ((paybackAmount >= (decimal)0.05))
            {
                var coin = new CoinModelResponse(Convert.ToInt32(paybackAmount / (decimal)0.05), "C05");
                paybackAmount = paybackAmount - (decimal)(coin.Quantity * 0.05);
                change.C05 = coin.Quantity;
                coins.Add(coin);
            }
            if ((paybackAmount >= (decimal)0.01))
            {
                var coin = new CoinModelResponse(Convert.ToInt32(paybackAmount / (decimal)0.01), "C01");
                paybackAmount = paybackAmount - (decimal)(coin.Quantity * 0.01);
                change.C01 = coin.Quantity;
                coins.Add(coin);
            }

            return coins;
        }

        private static List<BillModelResponse> GetBills(ref decimal paybackAmount, Change change)
        {

            var bills = new List<BillModelResponse>();

            if (paybackAmount >= 100)
            {
                var bill = new BillModelResponse(((int)paybackAmount / 100), "B100");
                paybackAmount = paybackAmount - (bill.Quantity * 100);
                change.B100 = bill.Quantity;
                bills.Add(bill);
            }
            if (paybackAmount >= 50)
            {
                var bill = new BillModelResponse(((int)paybackAmount / 50), "B50");
                paybackAmount = paybackAmount - (bill.Quantity * 50);
                change.B50 = bill.Quantity;
                bills.Add(bill);
            }
            if ((paybackAmount >= 20))
            {
                var bill = new BillModelResponse(((int)paybackAmount / 20), "B20");
                paybackAmount = paybackAmount - (bill.Quantity * 20);
                change.B20 = bill.Quantity;
                bills.Add(bill);
            }
            if ((paybackAmount >= 10))
            {
                var bill = new BillModelResponse(((int)paybackAmount / 10), "B10");
                paybackAmount = paybackAmount - (bill.Quantity * 10);
                change.B10 = bill.Quantity;
                bills.Add(bill);
            }

            return bills;
        }
    }
}
