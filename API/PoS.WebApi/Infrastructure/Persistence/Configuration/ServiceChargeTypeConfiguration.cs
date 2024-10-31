using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PoS.WebApi.Domain.Entities;

namespace PoS.WebApi.Infrastructure.Persistence.Configuration;

public class ServiceChargeTypeConfiguration : BaseEntityTypeConfiguration<ServiceCharge>
{
    public override void Configure(EntityTypeBuilder<ServiceCharge> builder)
    {
        base.Configure(builder);
        
        builder.Property(s => s.Name)
            .IsRequired();

        builder.Property(s => s.Description)
            .IsRequired();

        builder.Property(s => s.Value)
            .IsRequired();

        builder.Property(s => s.IsPercentage)
            .IsRequired();
        
        builder.Property(s => s.LastUpdated)
            .IsRequired();
    }
}