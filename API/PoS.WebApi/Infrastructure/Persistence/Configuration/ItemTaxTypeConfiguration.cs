using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PoS.WebApi.Domain.Entities;

namespace PoS.WebApi.Infrastructure.Persistence.Configuration;

public class ItemTaxTypeConfiguration : BaseEntityTypeConfiguration<ItemTax>
{
    public override void Configure(EntityTypeBuilder<ItemTax> builder)
    {
        base.Configure(builder);
        
        builder.HasOne(i => i.Tax)
            .WithMany()
            .HasForeignKey(i => i.TaxId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}