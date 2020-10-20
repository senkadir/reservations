using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reservations.Services.Reservations.Entities;

namespace Reservations.Services.Rooms.Data.EntityConfigurations
{
    public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.ToTable("reservations");

            builder.Property(x => x.RoomId)
                   .IsRequired();

            builder.HasIndex(x => x.Duration);
        }
    }
}
