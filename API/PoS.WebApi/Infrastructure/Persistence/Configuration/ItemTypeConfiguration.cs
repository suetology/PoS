using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PoS.WebApi.Domain.Entities;

namespace PoS.WebApi.Infrastructure.Persistence.Configuration;

public class ItemTypeConfiguration : BaseEntityTypeConfiguration<Item>
{
    public override void Configure(EntityTypeBuilder<Item> builder)
    {
        base.Configure(builder);
        
        builder.Property(i => i.Name)
            .IsRequired();
        
        builder.Property(i => i.Description)
            .IsRequired();
        
        builder.Property(i => i.Image)
            .IsRequired()
            .HasColumnType("BLOB");
        
        builder.Property(i => i.Price)
            .IsRequired();
        
        builder.Property(i => i.Stock)
            .IsRequired();

        builder.HasMany(i => i.Taxes)
            .WithMany();

        builder.HasMany(i => i.Discounts)
            .WithMany(d => d.Items);
    }
}