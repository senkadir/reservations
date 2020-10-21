using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Reservations.Common.Mvc
{
    public static class Extensions
    {
        public static IMvcBuilder SetupMvc(this IServiceCollection services)
        {
            return services.AddControllers(opt =>
            {
                opt.Filters.Add<ValidateModelAttribute>();
            });
        }

        public static IApplicationBuilder SetupPipeline(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ErrorHandlerMiddleware>();
        }
    }
}
