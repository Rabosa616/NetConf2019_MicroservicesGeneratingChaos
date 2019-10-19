using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Microservices.GeneratingChaos.BuildingBlocks.Extensions
{
   public static class SwaggerRegistrationExtension
    {
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
