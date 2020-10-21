using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reservations.Services.Rooms.Entities;

namespace Reservations.Services.Rooms.Data.EntityConfigurations
{
    public class ResourceConfiguration : IEntityTypeConfiguration<Resource>
    {
        public void Configure(EntityTypeBuilder<Resource> builder)
        {
            builder.ToTable("resources");

            builder.Property(x => x.Name)
                   .HasMaxLength(250);       
        }
    }
}
