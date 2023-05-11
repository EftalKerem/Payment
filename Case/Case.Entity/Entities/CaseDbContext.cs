using Case.Entity.Domains.Orders;
using Case.Entity.Domains.Payments; 
using Case.Entity.Domains.Users;
using Microsoft.EntityFrameworkCore;

namespace Case.Entity.Entities;

public class CaseDbContext : DbContext
{
    public CaseDbContext()
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }
    public DbSet<Order> Orders { get; set; } 
    public DbSet<PaymentTransaction> PaymentTransaction { get; set; } 
    public DbSet<User> Users { get; set; }  

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder
            .UseNpgsql("ConnectionString");
     
}