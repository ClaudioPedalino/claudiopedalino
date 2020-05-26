using System;
using System.Collections.Generic;
using System.Text;

namespace TotvsChallenge.Core.Helpers
{
    public static class MoneyHelper
    {
        public static readonly Dictionary<string, int> Bills = new Dictionary<string, int>
        {
            ["B100"] = 100,
            ["B50"] = 50,
            ["B20"] = 20,
            ["B10"] = 10,
        };

        public static readonly Dictionary<string, decimal> Coins = new Dictionary<string, decimal>
        {
            ["C050"] = (decimal)0.50,
            ["C010"] = (decimal)0.10,
            ["C005"] = (decimal)0.05,
            ["C001"] = (decimal)0.01,
        };

        public static readonly Dictionary<string, string> PaymentTypesDic = new Dictionary<string, string>
        {
            ["1"] = "Cash",
            ["2"] = "Debit Card",
            ["3"] = "Credit Card",
        };
    }
}