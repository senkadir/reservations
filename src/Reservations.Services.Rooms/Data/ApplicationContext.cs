using Microsoft.EntityFrameworkCore;
using Reservations.Services.Rooms.Entities;

namespace Reservations.Services.Rooms.Data
{
    public class ApplicationContext : DbContext
    {
        private readonly string _defaultSchema;

        public ApplicationContext(DbContextOptions<ApplicationContext> options, string defaultSchema = "rooms") : base(options)
        {
            _defaultSchema = defaultSchema;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(_defaultSchema);

            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Startup).Assembly);
        }

        public DbSet<Room> Rooms { get; set; }

        public DbSet<Resource> Resources { get; set; }

        public DbSet<RoomResource> RoomResources { get; set; }
    }
}
