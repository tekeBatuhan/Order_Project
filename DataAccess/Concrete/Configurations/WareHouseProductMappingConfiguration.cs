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
    public class WareHouseProductMappingConfiguration : IEntityTypeConfiguration<WareHouseProductMapping>
    {
        public void Configure(EntityTypeBuilder<WareHouseProductMapping> builder)
        {
            builder.HasKey(wp => wp.Id);
            builder.Property(wp => wp.Count).IsRequired();
            builder.Property(wp => wp.ReadyForSale).IsRequired();


            // ProjectBaseEntity'de tanımlanan ortak özelliklerin yapılandırması
            builder.Property(wp => wp.CreatedDate).IsRequired();
            builder.Property(wp => wp.LastUpdatedDate).IsRequired();
            builder.Property(wp => wp.Status).IsRequired();
            


            // WareHouseProductMapping ve WareHouse arasındaki ilişkinin yapılandırılması
            builder.HasOne(wp => wp.WareHouse)
                   .WithMany(w => w.WareHouseProductMappings)
                   .HasForeignKey(wp => wp.WareHouseId);

            // WareHouseProductMapping ve Product arasındaki ilişkinin yapılandırılması
            builder.HasOne(wp => wp.Product)
                   .WithMany(p => p.WareHouseProductMappings)
                   .HasForeignKey(wp => wp.ProductId);
        }
    }
}
