using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reservations.Services.Reservations.Entities;

namespace Reservations.Services.Reservations.Data.EntityConfigurations
{
    public class ResourceConfiguration : IEntityTypeConfiguration<Resource>
    {
        public void Configure(EntityTypeBuilder<Resource> builder)
        {
            builder.ToTable("resources");

            builder.HasOne(x => x.Reservation)
                   .WithMany(x => x.Resources)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
