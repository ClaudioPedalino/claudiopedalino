using System;

namespace TotvsChallenge.Entities
{
    public class Operation
    {
        public Guid Id { get; set; }
        //public Guid ProductId { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal ClientPaymentAmount { get; set; }
        public Guid ClientId { get; set; }
        public int PaymentTypeId { get; set; }
        public Guid? ChangeId { get; set; }

        public virtual Client Client { get; set; }
        public virtual PaymentType PaymentType { get; set; }
        public virtual Change Change { get; set; }

    }
}