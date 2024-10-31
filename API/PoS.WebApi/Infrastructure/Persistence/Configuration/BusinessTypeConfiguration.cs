using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PoS.WebApi.Domain.Entities;

namespace PoS.WebApi.Infrastructure.Persistence.Configuration;

public class BusinessTypeConfiguration : BaseEntityTypeConfiguration<Business>
{
    public override void Configure(EntityTypeBuilder<Business> builder)
    {
        base.Configure(builder);
        
        builder.Property(x => x.PhoneNumber)
            .IsRequired()
            .HasColumnType("VARCHAR(20)");
        
        builder.HasMany(b => b.Employees)
            .WithOne(u => u.Business)
            .HasForeignKey(u => u.BusinessId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}