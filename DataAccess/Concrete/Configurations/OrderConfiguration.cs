using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Concrete;

namespace DataAccess.Concrete.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);
            builder.Property(o => o.Quantity).IsRequired();
            

            // ProjectBaseEntity'de tanımlanan ortak özelliklerin yapılandırması
            builder.Property(o => o.CreatedDate).IsRequired();
            builder.Property(o => o.LastUpdatedDate).IsRequired();
            builder.Property(o => o.Status).IsRequired();

            // Order ve Customer arasındaki ilişkinin yapılandırılması
            builder.HasOne(o => o.Customer)
                   .WithMany(c => c.Orders)
                   .HasForeignKey(o => o.CustomerId);

            // Order ve Product arasındaki ilişkinin yapılandırılması
            builder.HasOne(o => o.Product)
                   .WithMany(p => p.Orders)
                   .HasForeignKey(o => o.ProductId);
        }
    }
}
