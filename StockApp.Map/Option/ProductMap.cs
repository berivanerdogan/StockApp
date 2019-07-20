using StockApp.Core.Map;
using StockApp.Model.Option;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockApp.Map.Option
{
    public class ProductMap:CoreMap<Product>
    {
        public ProductMap()
        {
            ToTable("dbo.Products");
            Property(x => x.ProductName).HasMaxLength(50).IsOptional();
            Property(x => x.Quantity).IsOptional();
            Property(x => x.CriticalStock).IsOptional();
            Property(x => x.FirstPrice).IsOptional();
            Property(x => x.SalePrice).IsOptional();
            Property(x => x.AddDate).HasColumnType("datetime2").IsOptional();
            Property(x => x.ImagePath).IsOptional();
            Property(x => x.LastProcessingDate).IsOptional();
            Property(x => x.RemainingQuantity).IsOptional();
            Property(x => x.SoldQuantity).IsOptional();

            HasRequired(x => x.Category)
              .WithMany(x => x.Products)
              .HasForeignKey(x => x.CategoryID)
              .WillCascadeOnDelete(false);
            
        }
    }
}
