using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Reservations.Common.Cache;
using Reservations.Common.Consul;
using Reservations.Common.Mvc;
using Reservations.Common.Swagger;

namespace Reservations.Services.Identity
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IHostEnvironment HostEnvironment { get; }

        public Startup(IConfiguration configuration, IHostEnvironment hostEnvironment)
        {
            Configuration = configuration;
            HostEnvironment = hostEnvironment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.SetupMvc()
                    .AddFluentValidation(x =>
                    {
                        x.RegisterValidatorsFromAssembly(typeof(Startup).Assembly);
                    })
                    .AddNewtonsoftJson();

            services.AddConsul()
                    .AddAutoMapper(typeof(Startup).Assembly)
                    .AddCaching();

            services.AddSwaggerDocs();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseAuthentication();

            app.SetupPipeline();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseConsul();

            app.UseSwaggerDocs();
        }
    }
}
