using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace Microservices.GeneratingChaos.BuildingBlocks.Extensions
{
    /// <summary>
    /// Class SwaggerRegistrationExtension.
    /// </summary>
    public static class SwaggerRegistrationExtension
    {
        /// <summary>
        /// Adds the swagger.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="version">The version.</param>
        /// <param name="title">The title.</param>
        /// <param name="description">The description.</param>
        /// <returns>IServiceCollection.</returns>
        public static IServiceCollection AddSwagger(this IServiceCollection services, string version, string title, string description)
        {
            services.AddSwaggerGen(options =>
            {
                options.DescribeAllEnumsAsStrings();
                options.SwaggerDoc(version,
                    new OpenApiInfo
                    {
                        Title = title,
                        Version = version,
                        Description = description
                    });

                // XML Documentation
                var xmlFile = $"{Assembly.GetEntryAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });
            return services;
        }

        /// <summary>
        /// Uses the swagger.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="name">The name.</param>
        /// <param name="version">The version.</param>
        /// <returns>IApplicationBuilder.</returns>
        public static IApplicationBuilder UseSwagger(this IApplicationBuilder app, string name, string version)
        {
            app.UseSwagger()
               .UseSwaggerUI(c =>
               {
                   c.SwaggerEndpoint($"/swagger/{version}/swagger.json", name);
               });
            return app;
        }
    }
}
