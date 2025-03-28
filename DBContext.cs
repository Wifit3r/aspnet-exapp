using Microsoft.EntityFrameworkCore;
using ASPNetExapp.Models;

public class DBContext : DbContext
{
    public DBContext(DbContextOptions<DBContext> options) : base(options) { }

    public DbSet<Newspaper> Newspapers { get; set; }
}
