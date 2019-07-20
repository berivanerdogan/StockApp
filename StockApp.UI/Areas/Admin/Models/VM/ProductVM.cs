using StockApp.Model.Option;
using StockApp.UI.Areas.Admin.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StockApp.UI.Areas.Admin.Models.VM
{
    public class ProductVM
    {
        public ProductVM()
        {
            Categories = new List<Category>();
            Products = new ProductDTO();
        }
        public List<Category> Categories { get; set; }
        public ProductDTO Products { get; set; }
    }
}