using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace TotvsChallengePoC.Entities
{
    public class Operation
    {
        
        public Guid Id { get; set; }
        //public Guid ProductId { get; set; }
        public DateTimeOffset DateCreated { get; set; }

        [Required]
        [MaxLength(20)]
        [Column(TypeName = "DECIMAL(10,2)")]
        public decimal TotalAmount { get; set; }
        
        [Required]
        [MaxLength(20)]
        [Column(TypeName = "DECIMAL(10,2)")]
        public decimal ClientPaymentAmount { get; set; }
        public Guid ClientId { get; set; }

        public int PaymentTypeId { get; set; }
        public Guid? ChangeId { get; set; }

        public virtual Client Client { get; set; }
        public virtual PaymentType PaymentType { get; set; }
        public virtual Change Change { get; set; }

    }
}
