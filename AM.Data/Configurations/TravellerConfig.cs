using AM.Core.Domaine;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AM.Data.Configurations
{
    public class TravellerConfig : IEntityTypeConfiguration<Traveller>
    {
        public void Configure(EntityTypeBuilder<Traveller> builder)
        {
            // Configuration de la table Traveller pour TPT
            builder.ToTable("Travellers");
        }
    }
}
