using Microsoft.EntityFrameworkCore;
using System;

namespace Reservations.Services.Tests.Common
{
    public class InMemoryDatabaseOptions<T> where T : DbContext
    {
        public DbContextOptions<T> Options { get; private set; }

        public InMemoryDatabaseOptions<T> New()
        {
            Options = UseEntityFrameworkInMemoryOptions();

            return this;
        }

        public InMemoryDatabaseOptions<T> Exists()
        {
            if (Options == null)
            {
                throw new ArgumentNullException(nameof(Options));
            }

            return this;
        }

        private DbContextOptions<T> UseEntityFrameworkInMemoryOptions()
        {
            return new DbContextOptionsBuilder<T>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
        }
    }
}
