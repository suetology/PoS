using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PoS.WebApi.Domain.Entities;

namespace PoS.WebApi.Infrastructure.Persistence.Configuration;

public class RefundTypeConfiguration : BaseEntityTypeConfiguration<Refund>
{
    public override void Configure(EntityTypeBuilder<Refund> builder)
    {
        base.Configure(builder);
        
        builder.Property(r => r.Amount)
            .IsRequired();
        
        builder.Property(r => r.Date)
            .IsRequired();

        builder.HasOne(r => r.Order)
            .WithOne(o => o.Refund)
            .HasForeignKey<Refund>(r => r.OrderId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}