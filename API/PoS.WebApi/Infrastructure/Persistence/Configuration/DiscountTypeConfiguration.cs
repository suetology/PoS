using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PoS.WebApi.Domain.Entities;

namespace PoS.WebApi.Infrastructure.Persistence.Configuration;

public class DiscountTypeConfiguration : BaseEntityTypeConfiguration<Discount>
{
    public override void Configure(EntityTypeBuilder<Discount> builder)
    {
        base.Configure(builder);
        
        builder.Property(d => d.Value)
            .IsRequired()
            .HasColumnType("DECIMAL(5, 2)");
        
        builder.Property(d => d.IsPercentage)
            .IsRequired();

        builder.Property(d => d.AmountAvailable)
            .IsRequired();

        builder.Property(d => d.ValidFrom)
            .IsRequired();
        
        builder.Property(d => d.ValidTo)
            .IsRequired();
        
        builder.HasMany(d => d.ItemDiscounts)
            .WithOne(i => i.Discount)
            .HasForeignKey(i => i.DiscountId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(d => d.GroupDiscounts)
            .WithOne(i => i.Discount)
            .HasForeignKey(i => i.DiscountId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}