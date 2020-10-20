using AutoMapper;
using FluentValidation.AspNetCore;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Reservations.Common.Broker;
using Reservations.Common.Cache;
using Reservations.Common.Consul;
using Reservations.Common.Swagger;
using Reservations.Services.Common.Types;
using Reservations.Services.Rooms.Data;

namespace Reservations.Services.Rooms
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
            services.AddControllers()
                    .AddFluentValidation(x =>
                    {
                        x.RegisterValidatorsFromAssembly(typeof(Startup).Assembly);
                    })
                    .AddNewtonsoftJson();

            services.AddConsul()
                  .AddBroker(
                  busConfigurator: c =>
                  {
                      c.AddConsumers(typeof(Startup).Assembly);
                  },
                  factoryConfigurator: (f, context) =>
                  {
                  })
                  .AddAutoMapper(typeof(Startup).Assembly)
                  .AddCaching();

            services.AddDbContext<ApplicationContext>(options =>
            {
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"), contextBuilderOptions =>
                {
                    contextBuilderOptions.MigrationsAssembly(typeof(Startup).Namespace);
                    contextBuilderOptions.MigrationsHistoryTable("ef_migrations_history", "public");
                })
                .UseSnakeCaseNamingConvention();

            })
            .AddScoped(typeof(DbContext), typeof(ApplicationContext));

            services.Scan(selector =>
            {
                selector.FromAssemblies(typeof(Startup).Assembly)
                        .AddClasses(c => c.AssignableTo<IBusinessBase>())
                        .AsImplementedInterfaces()
                        .WithTransientLifetime();
            });

            services.AddSwaggerDocs();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseConsul();

            app.UseSwaggerDocs();

            if (HostEnvironment.EnvironmentName == "Docker")
            {
            }
        }
    }
}
