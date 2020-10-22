using AutoMapper;
using FluentValidation.AspNetCore;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Reservations.Common.Authentication;
using Reservations.Common.Broker;
using Reservations.Common.Cache;
using Reservations.Common.Consul;
using Reservations.Common.Mvc;
using Reservations.Common.Swagger;
using Reservations.Services.Common.Types;
using Reservations.Services.Contracts.Events;
using Reservations.Services.Contracts.Requests;
using Reservations.Services.Offices.Business.Consumers;
using Reservations.Services.Offices.Data;
using Reservations.Services.Offices.Initializations;

namespace Reservations.Services.Offices
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
                  .AddBroker(
                  busConfigurator: c =>
                  {
                      c.AddConsumers(typeof(Startup).Assembly);
                  },
                  factoryConfigurator: (f, context) =>
                  {
                      f.ReceiveEndpoint(f.MessageTopology.EntityNameFormatter.FormatEntityName<OfficeCreated>(), e =>
                      {
                          e.Consumer<OfficeCreatedConsumer>(provider: context);
                      });

                      f.ReceiveEndpoint(f.MessageTopology.EntityNameFormatter.FormatEntityName<CheckOfficeExistence>(), e =>
                      {
                          e.Consumer<CheckOfficeExistenceConsumer>(provider: context);
                      });

                      f.ReceiveEndpoint(f.MessageTopology.EntityNameFormatter.FormatEntityName<CheckOfficeAvailability>(), e =>
                      {
                          e.Consumer<CheckOfficeAvailabilityConsumer>(provider: context);
                      });

                      f.ReceiveEndpoint(f.MessageTopology.EntityNameFormatter.FormatEntityName<CheckOfficeHoursAvailable>(), e =>
                      {
                          e.Consumer<CheckOfficeHoursAvailableConsumer>(provider: context);
                      });
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

            services.AddSecurity();
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

            if (HostEnvironment.EnvironmentName == "Docker")
            {
                app.InitializeService();
            }
        }
    }
}
