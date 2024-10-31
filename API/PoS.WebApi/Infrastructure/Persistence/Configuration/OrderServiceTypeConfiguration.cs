using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PoS.WebApi.Domain.Entities;

namespace PoS.WebApi.Infrastructure.Persistence.Configuration;

public class OrderServiceTypeConfiguration : BaseEntityTypeConfiguration<OrderService>
{
    public override void Configure(EntityTypeBuilder<OrderService> builder)
    {
        base.Configure(builder);
        
        builder.HasOne(o => o.Service)
            .WithMany()
            .HasForeignKey(o => o.ServiceId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}