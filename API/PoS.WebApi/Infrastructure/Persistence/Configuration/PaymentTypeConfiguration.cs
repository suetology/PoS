using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PoS.WebApi.Domain.Entities;

namespace PoS.WebApi.Infrastructure.Persistence.Configuration;

public class PaymentTypeConfiguration : BaseEntityTypeConfiguration<Payment>
{
    public override void Configure(EntityTypeBuilder<Payment> builder)
    {
        base.Configure(builder);
        
        builder.Property(p => p.Method)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(p => p.Amount)
            .IsRequired();
        
        builder.Property(p => p.StripeTransactionId)
            .IsRequired();
        
        builder.Property(p => p.Date)
            .IsRequired();
    }
}