using System;

namespace TotvsChallengePoC.Entities
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
    }
}
