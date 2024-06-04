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
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name).IsRequired().HasMaxLength(100); //Uzunluk Kısıtlamaları
            builder.Property(c => c.Code).IsRequired().HasMaxLength(20);
            builder.Property(c => c.Address).HasMaxLength(200);
            builder.Property(c => c.PhoneNumber).HasMaxLength(20);
            builder.Property(c => c.Email).HasMaxLength(50);

            // ProjectBaseEntity'de tanımlanan ortak özelliklerin yapılandırması
            builder.Property(c => c.CreatedDate).IsRequired();
            builder.Property(c => c.LastUpdatedDate).IsRequired();
            builder.Property(c => c.Status).IsRequired();

            // Customer ve Order arasındaki ilişkinin yapılandırılması
            builder.HasMany(c => c.Orders)
                   .WithOne(o => o.Customer)
                   .HasForeignKey(o => o.CustomerId);
        }
    }
}
