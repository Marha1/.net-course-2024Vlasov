using BankSystemDomain.Models;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.Data;

public class BankSystemDbContext : DbContext
{
    public DbSet<Client> Clients => Set<Client>();
    public DbSet<Account> Accounts => Set<Account>();
    public DbSet<Currency> Currencies => Set<Currency>();
    public DbSet<Employee> Employees => Set<Employee>();
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=Bank;Username=postgres;Password=053352287");
        
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BankSystemDbContext).Assembly);
    }
    
}