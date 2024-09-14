using ApiCar.Models;
using ApiCar.Notifications;
using Microsoft.EntityFrameworkCore;

namespace ApiCar.Data;

public class Context : DbContext
{
    public Context(DbContextOptions<Context> options) : base(options)
    {
        ChangeTracker.LazyLoadingEnabled = true;
    }

    public DbSet<Car> Cars { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<MaintenanceRecord> MaintenanceRecords { get; set; }
    public DbSet<InsurancePolicy> InsurancePolicy { get; set; }
    public DbSet<CarMileage> CarMilesages { get; set; }
    public DbSet<ChangeLog> ChangeLogs { get; set; }
    public DbSet<FuelRecord> FuelRecords { get; set; }
    public DbSet<CarListing> CarListings { get; set; }
    public DbSet<VehicleReport> VehicleReports { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Car>()
            .HasOne(c => c.Owner)
            .WithMany(u => u.Cars)
            .HasForeignKey(u => u.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Car>()
            .HasMany(m => m.MaintenanceRecords)
            .WithOne(c => c.Car)
            .HasForeignKey(m => m.CarId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Car>()
            .HasMany(i => i.InsurancePolicies)
            .WithOne(c => c.Car)
            .HasForeignKey(i => i.CarId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Car>()
            .HasMany(m => m.CarMileages)
            .WithOne(c => c.Car)
            .HasForeignKey(m => m.CarId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Car>()
            .HasMany(m => m.FuelRecords)
            .WithOne(m => m.Car)
            .HasForeignKey(m => m.CarId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<CarListing>()
            .HasOne(p => p.Car)
            .WithMany(p => p.CarListings)
            .HasForeignKey(p => p.CarId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<VehicleReport>()
            .HasOne(v => v.CarListing)
            .WithOne(c => c.VehicleReport)
            .HasForeignKey<VehicleReport>(v => v.CarListingId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Car>()
            .Property(c => c.Price)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<InsurancePolicy>()
            .Property(i => i.Premium)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Ignore<Notification>();

        modelBuilder.Entity<MaintenanceRecord>()
            .Ignore(p => p.Car);
    
        base.OnModelCreating(modelBuilder);
    }
}