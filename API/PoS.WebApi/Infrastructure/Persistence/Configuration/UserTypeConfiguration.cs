using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PoS.WebApi.Domain.Entities;

namespace PoS.WebApi.Infrastructure.Persistence.Configuration;

public class UserTypeConfiguration : BaseEntityTypeConfiguration<User>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        base.Configure(builder);
        
        builder.Property(u => u.Username)
            .IsRequired();

        builder.Property(u => u.PasswordHash)
            .IsRequired();
        
        builder.Property(u => u.Name)
            .IsRequired();
        
        builder.Property(u => u.Surname)
            .IsRequired();
        
        builder.Property(u => u.Email)
            .IsRequired();
        
        builder.Property(u => u.PhoneNumber)
            .HasColumnType("VARCHAR(20)")
            .IsRequired();
        
        builder.Property(u => u.Role)
            .HasConversion<int>()
            .IsRequired();
        
        builder.Property(u => u.Status)
            .HasConversion<int>()
            .IsRequired();
        
        builder.Property(u => u.DateOfEmployment)
            .IsRequired();
        
        builder.Property(u => u.LastUpdated)
            .IsRequired();
        
        builder.HasMany(u => u.Shifts)
            .WithOne(s => s.Employee)
            .HasForeignKey(s => s.EmployeeId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(u => u.Services)
            .WithOne(s => s.Employee)
            .HasForeignKey(u => u.EmployeeId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(u => u.Reservations)
            .WithOne(r => r.Employee)
            .HasForeignKey(r => r.EmployeeId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(u => u.Orders)
            .WithOne(o => o.Employee)
            .HasForeignKey(o => o.EmployeeId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}