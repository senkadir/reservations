using Microsoft.EntityFrameworkCore;

namespace Reservations.Services.Reservations.Data
{
    public class ApplicationContext : DbContext
    {
        private readonly string _defaultSchema;

        public ApplicationContext(DbContextOptions<ApplicationContext> options, string defaultSchema = "reservations") : base(options)
        {
            _defaultSchema = defaultSchema;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(_defaultSchema);

            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Startup).Assembly);
        }
    }
}
