namespace MyShuttle.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata;
    using MyShuttle.Model;

    public class MyShuttleContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public MyShuttleContext()
        {
        }

        public MyShuttleContext(DbContextOptions options)
        : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Customer>().HasKey(c => c.CustomerId);
            builder.Entity<Carrier>().HasKey(c => c.CarrierId);
            builder.Entity<Employee>().HasKey(e => e.EmployeeId);
            builder.Entity<Vehicle>().HasKey(v => v.VehicleId);
            builder.Entity<Driver>().HasKey(d => d.DriverId);

            builder.Entity<Ride>().HasKey(r => r.RideId);
            builder.Entity<Ride>()
                .HasOne(i => i.Driver)
                .WithMany(i => i.Rides)
                .HasForeignKey(i => i.DriverId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Ride>()
              .HasOne(i => i.Vehicle)
              .WithMany(i => i.Rides)
              .HasForeignKey(i => i.VehicleId)
              .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Vehicle>()
               .HasOne(p => p.Driver)
               .WithOne(i => i.Vehicle)
               .HasForeignKey<Driver>(b => b.VehicleId);


            base.OnModelCreating(builder);
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Carrier> Carriers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Ride> Rides { get; set; }
    }
}


