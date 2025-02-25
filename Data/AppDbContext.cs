using Microsoft.EntityFrameworkCore;
using ASPNetExapp.Models;

namespace ASPNetExapp.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Додаємо кілька початкових записів
        modelBuilder.Entity<User>().HasData(
            new User { Id = 1, Name = "John Doe", Email = "john@example.com" },
            new User { Id = 2, Name = "Jane Smith", Email = "jane@example.com" },
            new User { Id = 3, Name = "Alice Johnson", Email = "alice@example.com" },
            new User { Id = 4, Name = "Bob Brown", Email = "bob@example.com" }
        );
    }
}
