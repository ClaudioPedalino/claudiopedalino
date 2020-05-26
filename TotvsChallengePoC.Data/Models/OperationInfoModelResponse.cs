namespace TotvsChallengePoC.Data.Models
{
    public class OperationInfoModelResponse
    {
        public string DateCreated { get; set; }
        public decimal TotalAmount { get; set; }
        public string PaymentType { get; set; }
        public decimal ChangeReturned { get; set; }
        public string ClientFullName { get; set; }

    }
}
