using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configurations
{
    public class CustomerOrderConfiguration : IEntityTypeConfiguration<CustomerOrder>
    {
        public void Configure(EntityTypeBuilder<CustomerOrder> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired();

            builder.HasMany(x => x.SnacksOrder)
                    .WithOne(x => x.CustomerOrder)
                    .HasForeignKey(x => x.CustomerOrderId)
                    .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.DrinksOrder)
                    .WithOne(x => x.CustomerOrder)
                    .HasForeignKey(x => x.CustomerOrderId)
                    .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.DessertsOrder)
                    .WithOne(x => x.CustomerOrder)
                    .HasForeignKey(x => x.CustomerOrderId)
                    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
