using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PoS.WebApi.Domain.Entities;

namespace PoS.WebApi.Infrastructure.Persistence.Configuration;

public class GroupDiscountTypeConfiguration : BaseEntityTypeConfiguration<GroupDiscount>
{
    public override void Configure(EntityTypeBuilder<GroupDiscount> builder)
    {
        base.Configure(builder);
    }
}