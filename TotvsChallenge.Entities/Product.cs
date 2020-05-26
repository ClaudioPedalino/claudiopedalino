using System;
using System.Collections.Generic;
using System.Text;

namespace TotvsChallenge.Entities
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
    }
}
