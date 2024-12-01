using Microsoft.EntityFrameworkCore;
using PoS.WebApi.Domain.Entities;

namespace PoS.WebApi.Infrastructure.Persistence;

public class DatabaseContext : DbContext
{
    public DbSet<Business> Businesses { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Discount> Discounts { get; set; }
    public DbSet<GroupDiscount> GroupDiscounts { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<ItemDiscount> ItemDiscounts { get; set; }
    public DbSet<ItemGroup> ItemGroups { get; set; }
    public DbSet<ItemTax> ItemTaxes { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<OrderService> OrderServices { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Refund> Refunds { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<ServiceCharge> ServiceCharges { get; set; }
    public DbSet<Shift> Shifts { get; set; }
    public DbSet<Tax> Taxes { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    
    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DatabaseContext).Assembly);
    }
}