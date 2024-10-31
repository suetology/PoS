using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PoS.WebApi.Domain.Entities;

namespace PoS.WebApi.Infrastructure.Persistence.Configuration;

public class ShiftTypeConfiguration : BaseEntityTypeConfiguration<Shift>
{
    public override void Configure(EntityTypeBuilder<Shift> builder)
    {
        base.Configure(builder);
        
        builder.Property(s => s.Date)
            .IsRequired();
        
        builder.Property(s => s.StartTime)
            .IsRequired();
        
        builder.Property(s => s.EndTime)
            .IsRequired();
    }
}