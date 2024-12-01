using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PoS.WebApi.Domain.Entities;

namespace PoS.WebApi.Infrastructure.Persistence.Configuration;

public class RefreshTokenTypeConfiguration : BaseEntityTypeConfiguration<RefreshToken>
{
    public override void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        base.Configure(builder);
        
        builder.Property(t => t.AccessToken)
            .IsRequired();
        
        builder.Property(t => t.Expires)
            .IsRequired();
    }
}