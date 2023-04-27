using apibanca.application.Entities;
using Microsoft.EntityFrameworkCore;
 
namespace apibanca.application.Infrastructure.Data;

public class ApiContext : DbContext
{
    public ApiContext(DbContextOptions<ApiContext> options)
        : base(options)
    {
    }
    public virtual DbSet<User> Users { get; set; } = null!;
    public virtual DbSet<Account> Accounts { get; set; } = null!;
    protected override void OnModelCreating(ModelBuilder modelBuilder) {  
        modelBuilder.Entity<User>().HasKey(p => p.IDUser);
        modelBuilder.Entity<Account>().HasKey(p => p.IDAccount);
        modelBuilder.SeedData();
        base.OnModelCreating(modelBuilder);  
    }        
}
