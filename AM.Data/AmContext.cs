using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AM.Core.Domaine;
using AM.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace AM.Data
{
    public class AmContext : DbContext
    {
        public DbSet<Flight> flights { get; set; }
        public DbSet<Passenger> passengers { get; set; }
        public DbSet<Plane> planes { get; set; }
        public DbSet<Staff> staffs { get; set; }
        public DbSet<Traveller> travellers { get; set; }
        public DbSet<Reservation> reservations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLazyLoadingProxies() // Activer le LazyLoading
                .UseSqlServer(
                    @"Data Source=(localdb)\mssqllocaldb; Initial Catalog = Airport; Integrated Security = true"
                );
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PlaneConfig());
            modelBuilder.ApplyConfiguration(new FlightConfig());
            modelBuilder.ApplyConfiguration(new PassengerConfig());
            modelBuilder.ApplyConfiguration(new ReservationConfig()); // Ajout de ReservationConfig

            // Ces configurations ne sont plus nu00e9cessaires avec TPH
            // modelBuilder.ApplyConfiguration(new StaffConfig());     // Ajout de StaffConfig
            // modelBuilder.ApplyConfiguration(new TravellerConfig()); // Ajout de TravellerConfig

            base.OnModelCreating(modelBuilder);
        }
    }
}
