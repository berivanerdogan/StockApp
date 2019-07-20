using StockApp.Core.Map;
using StockApp.Model.Option;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockApp.Map.Option
{
    public class CategoryMap:CoreMap<Category>
    {
        public CategoryMap()
        {
            ToTable("dbo.Categories");
            Property(x => x.Description).HasColumnName("Description").HasMaxLength(50).IsOptional();
            Property(x => x.Name).HasColumnName("Category Name").HasMaxLength(50).IsOptional();

            HasMany(x => x.Products)
                .WithRequired(x => x.Category)
                .HasForeignKey(x => x.CategoryID).WillCascadeOnDelete(false);
        }
    }
}
