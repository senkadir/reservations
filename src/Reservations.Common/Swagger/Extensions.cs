using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;

namespace Reservations.Common.Swagger
{
    public static class Extensions
    {
        public static IServiceCollection AddSwaggerDocs(this IServiceCollection services)
        {
            SwaggerOptions options = new SwaggerOptions();

            using (var serviceProvider = services.BuildServiceProvider())
            {
                var configuration = serviceProvider.GetService<IConfiguration>();

                services.Configure<SwaggerOptions>(configuration.GetSection("swagger"));

                configuration.GetSection("swagger").Bind(options);
            }

            if (!options.Enabled)
            {
                return services;
            }

            return services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = options.Title, Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description =
                        "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                },
                                Name = "Bearer",
                                Scheme = "oauth2",
                                In = ParameterLocation.Header
                            },
                            new string[] { }
                        }
                    });

                var xmlPath = Path.Combine(AppContext.BaseDirectory, options.XmlFileName);

                c.IncludeXmlComments(xmlPath);

            });
        }

        public static IApplicationBuilder UseSwaggerDocs(this IApplicationBuilder builder)
        {
            SwaggerOptions options = new SwaggerOptions();

            var configuration = builder.ApplicationServices.GetService<IConfiguration>();

            configuration.GetSection("swagger").Bind(options);

            if (!options.Enabled)
            {
                return builder;
            }

            builder.UseSwagger(c => c.RouteTemplate = "docs/{documentName}/swagger.json");

            builder.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/docs/v1/swagger.json", "Offices service documentation");
                c.RoutePrefix = "docs";
            });

            builder.UseReDoc(c =>
            {
                c.RoutePrefix = "docs/redoc";
                c.SpecUrl = "/docs/v1/swagger.json";
            });

            return builder;
        }
    }
}
