using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PoS.WebApi.Domain.Entities;

namespace PoS.WebApi.Infrastructure.Persistence.Configuration;

public class ServiceTypeConfiguration : BaseEntityTypeConfiguration<Service>
{
    public override void Configure(EntityTypeBuilder<Service> builder)
    {
        base.Configure(builder);
        
        builder.Property(s => s.Name)
            .IsRequired();
        
        builder.Property(s => s.Description)
            .IsRequired();

        builder.Property(s => s.Price)
            .IsRequired();
        
        builder.Property(s => s.Duration)
            .IsRequired();
        
        builder.Property(s => s.IsActive)
            .HasDefaultValue(false)
            .IsRequired();
    }
}