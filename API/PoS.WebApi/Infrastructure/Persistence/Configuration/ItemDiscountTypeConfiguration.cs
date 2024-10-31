using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PoS.WebApi.Domain.Entities;

namespace PoS.WebApi.Infrastructure.Persistence.Configuration;

public class ItemDiscountTypeConfiguration : BaseEntityTypeConfiguration<ItemDiscount>
{
    public override void Configure(EntityTypeBuilder<ItemDiscount> builder)
    {
        base.Configure(builder);
    }
}