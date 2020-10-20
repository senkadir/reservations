using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Reservations.Services.Reservations.Data;

namespace Reservations.Services.Reservations.Initializations
{
    public static class Extensions
    {
        public static void InitializeService(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();

            var context = serviceScope.ServiceProvider.GetService<ApplicationContext>();

            context.Database.Migrate();
        }
    }
}
