using System;

namespace AccountManagement.Models.DTOs.ReportsDto
{
    public class ProductByCategoryDto
    {
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
