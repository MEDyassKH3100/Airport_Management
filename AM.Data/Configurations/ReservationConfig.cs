using AM.Core.Domaine;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AM.Data.Configurations
{
    public class ReservationConfig : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            // Configuration de la table Reservation
            builder.ToTable("Reservations");

            // Configuration de la clé primaire
            builder.HasKey(r => r.Id);

            // Configuration des propriétés
            builder.Property(r => r.Price).IsRequired();
            builder.Property(r => r.Seat).IsRequired();
            builder.Property(r => r.VIP).IsRequired();

            // Configuration des relations
            builder
                .HasOne(r => r.Passenger)
                .WithMany(p => p.Reservations)
                .HasForeignKey(r => r.PassengerFK)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(r => r.Flight)
                .WithMany(f => f.Reservations)
                .HasForeignKey(r => r.FlightFK)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
