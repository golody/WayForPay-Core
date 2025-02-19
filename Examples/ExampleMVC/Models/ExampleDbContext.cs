using Microsoft.EntityFrameworkCore;

namespace ExampleMVC.Models;

public class ExampleDbContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<Order> Orders { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
    { 
        optionsBuilder.UseInMemoryDatabase("TestDb"); 
        optionsBuilder.EnableSensitiveDataLogging();
    } 
}