using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TotvsChallenge.Data.Models;

namespace TotvsChallenge.Core.Request.Operations
{
    public class BuyRequest : IRequest<ChangeModelResponse>
    {
        public IList<Product> Products { get; set; }
        public string ClientId { get; set; }
        public int PaymentType { get; set; }
        public decimal TotalAmount { get { return Products.Sum(x => x.Amount); } }
        public decimal ClientPaymentAmount { get; set; }
    }

    public class Product
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }

    }
}
