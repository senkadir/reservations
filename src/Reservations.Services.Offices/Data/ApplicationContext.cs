using Microsoft.EntityFrameworkCore;
using Reservations.Services.Offices.Entities;

namespace Reservations.Services.Offices.Data
{
    public class ApplicationContext : DbContext
    {
        private readonly string _defaultSchema;

        public ApplicationContext(DbContextOptions<ApplicationContext> options, string defaultSchema = "offices") : base(options)
        {
            _defaultSchema = defaultSchema;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(_defaultSchema);

            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Startup).Assembly);
        }

        public DbSet<Office> Offices { get; set; }
    }
}
