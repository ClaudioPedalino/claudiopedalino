using System;
using System.Collections.Generic;
using System.Text;

namespace TotvsChallenge.Data.Models
{
    public class ChangeModelResponse
    {
        public decimal Amount { get; set; }
        public ICollection<BillModelResponse> Bills { get; set; }
        public ICollection<CoinModelResponse> Coins { get; set; }
    }

    public class BillModelResponse
    {
        public BillModelResponse(int quantity, string type)
        {
            Type = type ?? throw new System.ArgumentNullException(nameof(type));
            Quantity = quantity;
        }

        public int Quantity { get; set; }
        public string Type { get; set; }
    }

    public class CoinModelResponse
    {
        public CoinModelResponse(int quantity, string type)
        {
            Type = type ?? throw new System.ArgumentNullException(nameof(type));
            Quantity = quantity;
        }

        public int Quantity { get; set; }
        public string Type { get; set; }
    }
}