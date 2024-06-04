using Entities.Concrete;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
            // Boyut enum'u için dönüşüm
            builder.Property(p => p.Size).IsRequired().HasConversion<int>();

            // Renk ve ilişkili dış anahtar
            builder.HasOne(p => p.Color)
                   .WithMany(c => c.Products)
                   .HasForeignKey(p => p.ColorId);

            // ProjectBaseEntity'de tanımlanan ortak özelliklerin yapılandırması
            builder.Property(p => p.CreatedDate).IsRequired();
            builder.Property(p => p.LastUpdatedDate).IsRequired();
            builder.Property(p => p.Status).IsRequired();

            // Ürün ve Depo-Ürün eşlemeleri arasındaki ilişkinin yapılandırılması
            builder.HasMany(p => p.WareHouseProductMappings)
                   .WithOne(wp => wp.Product)
                   .HasForeignKey(wp => wp.ProductId);

            // Ürün ve Siparişler arasındaki ilişkinin yapılandırılması
            builder.HasMany(p => p.Orders)
                   .WithOne(o => o.Product)
                   .HasForeignKey(o => o.ProductId);
        }
    }
}
