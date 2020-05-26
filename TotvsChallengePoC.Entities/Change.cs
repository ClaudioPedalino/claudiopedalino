using System;

namespace TotvsChallengePoC.Entities
{
    public class Change
    {
        public Change(Guid id, decimal amountToReturn)
        {
            Id = id;
            AmountToReturn = amountToReturn;
        }

        public Guid Id { get; set; }
        public decimal AmountToReturn { get; set; }
        public int? B100 { get; set; }
        public int? B50 { get; set; }
        public int? B20 { get; set; }
        public int? B10 { get; set; }
        public int? C50 { get; set; }
        public int? C10 { get; set; }
        public int? C05 { get; set; }
        public int? C01 { get; set; }

    }
}
