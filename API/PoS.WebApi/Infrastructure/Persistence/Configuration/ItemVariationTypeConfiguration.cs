using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PoS.WebApi.Domain.Entities;

namespace PoS.WebApi.Infrastructure.Persistence.Configuration;

public class ItemVariationTypeConfiguration : BaseEntityTypeConfiguration<ItemVariation>
{
    public override void Configure(EntityTypeBuilder<ItemVariation> builder)
    {
        base.Configure(builder);
        
        builder.Property(i => i.Name)
            .IsRequired();
        
        builder.Property(i => i.AddedPrice)
            .IsRequired();
        
        builder.Property(i => i.Stock)
            .IsRequired();
        
        builder.HasOne(i => i.Item)
            .WithMany(i => i.ItemVariations)
            .HasForeignKey(i => i.ItemId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany<OrderItem>()
            .WithMany(o => o.ItemVariations);
    }
}