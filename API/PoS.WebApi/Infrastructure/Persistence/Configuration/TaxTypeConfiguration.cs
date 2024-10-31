using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PoS.WebApi.Domain.Entities;

namespace PoS.WebApi.Infrastructure.Persistence.Configuration;

public class TaxTypeConfiguration : BaseEntityTypeConfiguration<Tax>
{
    public override void Configure(EntityTypeBuilder<Tax> builder)
    {
        base.Configure(builder);
        
        builder.Property(t => t.Name)
            .IsRequired();

        builder.Property(t => t.Type)
            .HasConversion<int>()
            .IsRequired();
        
        builder.Property(t => t.Value)
            .IsRequired();
        
        builder.Property(t => t.IsPercentage)
            .IsRequired();
        
        builder.Property(t => t.LastUpdated)
            .IsRequired();
    }
}