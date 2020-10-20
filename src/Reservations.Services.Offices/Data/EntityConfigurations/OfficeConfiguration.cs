using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reservations.Services.Offices.Entities;

namespace Reservations.Services.Offices.Data.EntityConfigurations
{
    public class OfficeConfiguration : IEntityTypeConfiguration<Office>
    {
        public void Configure(EntityTypeBuilder<Office> builder)
        {
            builder.ToTable("offices");

            builder.Property(x => x.Location)
                   .HasMaxLength(250)
                   .IsRequired();

            builder.Property(x => x.OpenTime)
                   .IsRequired();

            builder.Property(x => x.CloseTime)
                   .IsRequired();
        }
    }
}
