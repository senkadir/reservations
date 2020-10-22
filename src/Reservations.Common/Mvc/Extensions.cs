using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace Reservations.Common.Mvc
{
    public static class Extensions
    {
        public static IMvcBuilder SetupMvc(this IServiceCollection services)
        {
            var policy = new AuthorizationPolicyBuilder()
                              .RequireAuthenticatedUser()
                              .Build();

            return services.AddControllers(opt =>
            {
                opt.Filters.Add<ValidateModelAttribute>();
                opt.Filters.Add(new AuthorizeFilter(policy));
            });
        }

        public static IApplicationBuilder SetupPipeline(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ErrorHandlerMiddleware>();
        }
    }
}
