using System;

namespace ListatTestProject_BL.DTOs
{
    public class SaleDto
    {
        public string Name { get; set; }
        public DateTime CreatedDt { get; set; }
        public DateTime? FinishedDt { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; }
        public string Seller { get; set; }
        public string Buyer { get; set; }
    }
}
