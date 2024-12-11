using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PoS.WebApi.Domain.Entities;

namespace PoS.WebApi.Infrastructure.Persistence.Configuration;

public class OrderTypeConfiguration : BaseEntityTypeConfiguration<Order>
{
    public override void Configure(EntityTypeBuilder<Order> builder)
    {
        base.Configure(builder);
        
        builder.Property(o => o.Status)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(o => o.Created)
            .IsRequired();
        
        builder.HasMany(o => o.OrderItems)
            .WithOne(o => o.Order)
            .HasForeignKey(o => o.OrderId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(o => o.Payments)
            .WithOne(o => o.Order)
            .HasForeignKey(o => o.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(o => o.ServiceCharge)
            .WithMany()
            .HasForeignKey(o => o.ServiceChargeId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(o => o.Customer)
            .WithMany(c => c.Orders)
            .HasForeignKey(o => o.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(o => o.Discount)
            .WithMany(d => d.Orders)
            .HasForeignKey(o => o.DiscountId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}