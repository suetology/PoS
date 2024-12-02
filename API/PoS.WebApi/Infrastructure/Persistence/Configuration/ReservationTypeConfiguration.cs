using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PoS.WebApi.Domain.Entities;

namespace PoS.WebApi.Infrastructure.Persistence.Configuration;

public class ReservationTypeConfiguration : BaseEntityTypeConfiguration<Reservation>
{
    public override void Configure(EntityTypeBuilder<Reservation> builder)
    {
        base.Configure(builder);
        
        builder.Property(r => r.ReservationTime)
            .IsRequired();
        
        builder.Property(r => r.AppointmentTime)
            .IsRequired();
        
        builder.Property(r => r.Status)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(r => r.NotificationSent)
            .IsRequired();

        builder.HasOne(r => r.Order)
            .WithOne(o => o.Reservation)
            .HasForeignKey<Reservation>(r => r.OrderId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(r => r.Service)
            .WithMany(s => s.Reservations)
            .HasForeignKey(r => r.ServiceId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}