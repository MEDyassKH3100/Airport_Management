using AM.Core.Domaine; // Correction du namespace
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AM.Data.Configurations // Correction du namespace
{
    public class PassengerConfig : IEntityTypeConfiguration<Passenger>
    {
        public void Configure(EntityTypeBuilder<Passenger> builder)
        {
            // Configuration de FullName comme type d'entitu00e9 du00e9tenue
            builder.OwnsOne(
                p => p.FullName,
                fn =>
                {
                    fn.Property(f => f.FirstName).HasMaxLength(30).HasColumnName("Name");
                    fn.Property(f => f.LastName).IsRequired();
                }
            );

            // Configuration de l'hu00e9ritage TPT (Table Per Type)
            // Configuration de l'hu00e9ritage TPH (Table Per Hierarchy)
            builder.ToTable("Passengers");
            // Supprimer la ligne : builder.UseTptMappingStrategy();

            // La configuration du discriminateur n'est plus nu00e9cessaire avec TPT
            // car chaque type a sa propre table
            // Configuration du discriminateur selon les spu00e9cifications
            // Décommenter la configuration du discriminateur
            builder
                .HasDiscriminator<int>("IsTraveller")
                .HasValue<Traveller>(1) // a) valeur 1 pour Traveller
                .HasValue<Staff>(2) // b) valeur 2 pour Staff
                .HasValue<Passenger>(0); // c) valeur 0 pour les autres
        }
    }
}
