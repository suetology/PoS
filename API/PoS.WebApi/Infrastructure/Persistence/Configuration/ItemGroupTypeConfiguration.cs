using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PoS.WebApi.Domain.Entities;

namespace PoS.WebApi.Infrastructure.Persistence.Configuration;

public class ItemGroupTypeConfiguration : BaseEntityTypeConfiguration<ItemGroup>
{
    public override void Configure(EntityTypeBuilder<ItemGroup> builder)
    {
        base.Configure(builder);
        
        builder.HasMany(i => i.Items)
            .WithOne(i => i.ItemGroup)
            .HasForeignKey(i => i.ItemGroupId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(i => i.GroupDiscounts)
            .WithOne(g => g.ItemGroup)
            .HasForeignKey(g => g.ItemGroupId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}