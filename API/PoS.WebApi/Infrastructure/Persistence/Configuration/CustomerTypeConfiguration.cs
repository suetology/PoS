using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PoS.WebApi.Domain.Entities;

namespace PoS.WebApi.Infrastructure.Persistence.Configuration;

public class CustomerTypeConfiguration : BaseEntityTypeConfiguration<Customer>
{
    public override void Configure(EntityTypeBuilder<Customer> builder)
    {
        base.Configure(builder);
        
        builder.HasKey(c => c.Id);
        
        builder.Property(c => c.PhoneNumber)
            .HasColumnType("VARCHAR(20)");

        builder.HasMany(c => c.Reservations)
            .WithOne(r => r.Customer)
            .HasForeignKey(r => r.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}