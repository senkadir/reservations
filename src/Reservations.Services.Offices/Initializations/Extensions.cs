using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Reservations.Services.Offices.Data;
using Reservations.Services.Offices.Entities;
using System;

namespace Reservations.Services.Offices.Initializations
{
    public static class Extensions
    {
        public static void InitializeService(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();

            var context = serviceScope.ServiceProvider.GetService<ApplicationContext>();

            var logger = serviceScope.ServiceProvider.GetService<ILogger<Startup>>();

           context.Database.Migrate();

            context.Offices.Add(new Office
            {
                Id = Guid.NewGuid(),
                Location = "Amsterdam",
                OpenTime = new TimeSpan(8, 30, 0),
                CloseTime = new TimeSpan(17, 0, 0)
            });

            context.Offices.Add(new Office
            {
                Id = Guid.NewGuid(),
                Location = "Berlin",
                OpenTime = new TimeSpan(8, 30, 0),
                CloseTime = new TimeSpan(21, 0, 0)
            });

            context.SaveChanges();

            logger.LogDebug("Service initialization completed.");
        }
    }
}
