using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CarMaintenanceApp.Models
{
    public class CarMaintenanceContext : DbContext
    {
        private readonly ILogger<CarMaintenanceContext> _logger;
        public CarMaintenanceContext(DbContextOptions<CarMaintenanceContext> options, ILogger<CarMaintenanceContext> logger) : base(options)
        {
            _logger = logger;
            _logger.LogInformation("CarMaintenanceContext instantiated.");
        }
        public DbSet<Car> Cars { get; set; }
        public DbSet<ServiceRecord> ServiceRecords { get; set; }
        
        public DbSet<ApplicationUser> UserAccounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>()
                .HasOne<ApplicationUser>()
                .WithMany(u => u.Cars)
                .HasForeignKey(c => c.UserId);
            modelBuilder.Entity<ServiceRecord>()
                .HasOne(s => s.Car)
                .WithMany(c => c.ServiceRecords)
                .HasForeignKey(s => s.CarId);
        }
    }
}
