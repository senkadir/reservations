using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reservations.Services.Rooms.Entities;

namespace Reservations.Services.Rooms.Data.EntityConfigurations
{
    public class RoomConfiguration : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.ToTable("rooms");

            builder.Property(x => x.OfficeId)
                   .IsRequired();

            builder.Property(x => x.Name)
                   .HasMaxLength(250)
                   .IsRequired();
        }
    }
}
