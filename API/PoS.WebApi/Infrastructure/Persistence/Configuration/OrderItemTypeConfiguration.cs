using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PoS.WebApi.Domain.Entities;

namespace PoS.WebApi.Infrastructure.Persistence.Configuration;

public class OrderItemTypeConfiguration : BaseEntityTypeConfiguration<OrderItem>
{
    public override void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        base.Configure(builder);
        
        builder.Property(o => o.Quantity)
            .HasDefaultValue(0)
            .IsRequired();
        
        builder.HasOne(o => o.Item)
            .WithMany()
            .HasForeignKey(o => o.ItemId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}