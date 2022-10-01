using System;

namespace ListatTestProject_DAL.Models
{
    public class Sale : BaseEntity
    {
        public int ItemId { get; set; }
        public DateTime CreatedDt { get; set; }
        public DateTime? FinishedDt { get; set; }
        public decimal Price { get; set; }
        public MarketStatus Status { get; set; }
        public string Seller { get; set; }
        public string Buyer { get; set; }
    }
}
