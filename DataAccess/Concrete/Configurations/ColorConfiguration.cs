using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
//using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities.Concrete;
using Entities.Concrete;


namespace DataAccess.Concrete.Configurations
{
    public class ColorConfiguration : IEntityTypeConfiguration<Color>
    {
        public void Configure(EntityTypeBuilder<Color> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name).IsRequired().HasMaxLength(50);
            builder.Property(c => c.CreatedDate).IsRequired();
            builder.Property(c => c.LastUpdatedDate).IsRequired();
            builder.Property(c => c.Status).IsRequired();

            //Renklerin ürünlerle ilişkilendirilmesi için bir örnek:
             builder.HasMany(c => c.Products)
                    .WithOne(p => p.Color)
                    .HasForeignKey(p => p.ColorId);
        }
    }
}
