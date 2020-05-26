using System;
using System.Collections.Generic;
using System.Text;

namespace TotvsChallenge.Data.Models
{
    public class ClientInfoModelResponse
    {
        public string ClientFullName { get; set; }
        public decimal TotalSpend { get; set; }
        public int CashTimes { get; set; }
        public int DebitTimes { get; set; }
        public int CreditTimes { get; set; }
    }
}