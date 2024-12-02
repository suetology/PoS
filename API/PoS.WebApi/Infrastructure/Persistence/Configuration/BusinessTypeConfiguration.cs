using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PoS.WebApi.Domain.Entities;

namespace PoS.WebApi.Infrastructure.Persistence.Configuration;

public class BusinessTypeConfiguration : BaseEntityTypeConfiguration<Business>
{
    public override void Configure(EntityTypeBuilder<Business> builder)
    {
        base.Configure(builder);
        
        builder.Property(x => x.PhoneNumber)
            .IsRequired()
            .HasColumnType("VARCHAR(20)");
        
        builder.HasMany(b => b.Employees)
            .WithOne(u => u.Business)
            .HasForeignKey(u => u.BusinessId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired(false);

        builder.HasMany<Customer>()
            .WithOne()
            .HasForeignKey(c => c.BusinessId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany<Discount>()
            .WithOne()
            .HasForeignKey(d => d.BusinessId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany<ItemGroup>()
            .WithOne()
            .HasForeignKey(i => i.BusinessId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany<Item>()
            .WithOne()
            .HasForeignKey(i => i.BusinessId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany<OrderItem>()
            .WithOne()
            .HasForeignKey(o => o.BusinessId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany<Order>()
            .WithOne()
            .HasForeignKey(o => o.BusinessId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany<Payment>()
            .WithOne()
            .HasForeignKey(p => p.BusinessId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany<Refund>()
            .WithOne()
            .HasForeignKey(r => r.BusinessId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany<Reservation>()
            .WithOne()
            .HasForeignKey(r => r.BusinessId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany<ServiceCharge>()
            .WithOne()
            .HasForeignKey(s => s.BusinessId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany<Service>()
            .WithOne()
            .HasForeignKey(s => s.BusinessId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany<Shift>()
            .WithOne()
            .HasForeignKey(s => s.BusinessId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany<Tax>()
            .WithOne()
            .HasForeignKey(t => t.BusinessId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}