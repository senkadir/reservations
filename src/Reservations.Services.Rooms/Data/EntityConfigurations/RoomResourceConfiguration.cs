using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reservations.Services.Rooms.Entities;

namespace Reservations.Services.Rooms.Data.EntityConfigurations
{
    public class RoomResourceConfiguration : IEntityTypeConfiguration<RoomResource>
    {
        public void Configure(EntityTypeBuilder<RoomResource> builder)
        {
            builder.ToTable("room_resources");

            builder.HasOne(x => x.Room)
                   .WithMany(x => x.RoomResources)
                   .HasForeignKey(x => x.RoomId);
        }
    }
}
