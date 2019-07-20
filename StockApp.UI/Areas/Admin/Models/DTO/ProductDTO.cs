using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StockApp.UI.Areas.Admin.Models.DTO
{
    public class ProductDTO
    {
        public Guid ID { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public int CriticalStock { get; set; }
        public decimal FirstPrice { get; set; }
        public decimal SalePrice { get; set; }
        public DateTime? AddDate { get; set; }
        public string ImagePath { get; set; }
        public int SoldQuantity { get; set; }

        public int RemainingQuantity { get; set; }
        public DateTime? LastProcessingDate { get; set; }

        public Guid CategoryID { get; set; }
    }
}