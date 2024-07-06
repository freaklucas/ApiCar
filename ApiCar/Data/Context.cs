using ApiCar.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiCar.Data;

public class Context : DbContext
{
    public Context(DbContextOptions<Context> options) : base(options)
    {
        this.ChangeTracker.LazyLoadingEnabled = true;
    }
    
    public DbSet<Car> Cars { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Car>()
            .HasOne(c => c.Owner)
            .WithMany(u => u.Cars)
            .HasForeignKey(u => u.UserId)
            .OnDelete(DeleteBehavior.Restrict);
        
        base.OnModelCreating(modelBuilder);
    }
}