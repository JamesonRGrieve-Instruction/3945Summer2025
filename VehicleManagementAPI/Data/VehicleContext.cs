using Microsoft.EntityFrameworkCore;
using VehicleManagementAPI.Models;

namespace VehicleManagementAPI.Data
{
    public class VehicleContext : DbContext
    {
        public VehicleContext(DbContextOptions<VehicleContext> options) : base(options) { }
        
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Model>()
                .HasOne(m => m.Manufacturer)
                .WithMany(man => man.Models)
                .HasForeignKey(m => m.ManufacturerID);
                
            modelBuilder.Entity<Vehicle>()
                .HasOne(v => v.Model)
                .WithMany(m => m.Vehicles)
                .HasForeignKey(v => v.ModelID);
                
            modelBuilder.Entity<Vehicle>()
                .HasKey(v => v.VIN);
        }
    }
}