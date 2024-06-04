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
    public class WareHouseConfiguration : IEntityTypeConfiguration<WareHouse>
    {
        public void Configure(EntityTypeBuilder<WareHouse> builder)
        {
            builder.HasKey(w => w.Id);
            builder.Property(w => w.Name).IsRequired().HasMaxLength(100);

            // ProjectBaseEntity'de tanımlanan ortak özelliklerin yapılandırması
            builder.Property(w => w.CreatedDate).IsRequired();
            builder.Property(w => w.LastUpdatedDate).IsRequired();
            builder.Property(w => w.Status).IsRequired();

            // Depo ve Depo-Ürün eşlemeleri arasındaki ilişkinin yapılandırılması
            builder.HasMany(w => w.WareHouseProductMappings)
                   .WithOne(wp => wp.WareHouse)
                   .HasForeignKey(wp => wp.WareHouseId);
        }
    }
}
