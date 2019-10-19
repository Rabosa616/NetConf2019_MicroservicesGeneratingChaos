using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microservices.GeneratingChaos.BuildingBlocks.Extensions;
using Microservices.GeneratingChaos.BuildingBlocks.Infrastructure.DataBase;
using Microservices.GeneratingChaos.Services.Api.Domain.Entities;
using Microservices.GeneratingChaos.Services.Api.Infrastructure.AutofacModules;
using Microservices.GeneratingChaos.Services.Api.Infrastructure.Repository.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Microservices.GeneratingChaos.Services.Api
{
    public class Startup
    {
        /// <summary>Gets the configuration.</summary>
        /// <value>The configuration.</value>
        public IConfiguration Configuration { get; }
     
        /// <summary>Initializes a new instance of the <see cref="Startup"/> class.</summary>
        /// <param name="configuration">The configuration.</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>Configures the services.</summary>
        /// <param name="services">The services.</param>
        public virtual IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddCustomMvc<Startup>()
                    .AddSwagger("v1", "Microservices Generating Chaos Api", "Sample API for netconf 2019")
                    .AddOptions();

            var containerBuilder = new ContainerBuilder();
            containerBuilder.Populate(services);
            var factory = new DbFactory(Configuration.Get<DbConfiguration>());
            var assembly = typeof(Startup).Assembly;
            containerBuilder.RegisterModule(new MediatorModule());
            containerBuilder.RegisterModule(new ApplicationModule(Configuration));
            containerBuilder.RegisterRepositoryModule(assembly, factory);
            return new AutofacServiceProvider(containerBuilder.Build());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            var pathBase = Configuration["PATH_BASE"];
            if (!string.IsNullOrEmpty(pathBase))
            {
                loggerFactory.CreateLogger<Startup>()
                             .LogDebug("Using PATH BASE '{pathBase}'", pathBase);
                app.UsePathBase(pathBase);
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("CorsPolicy");
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapControllers();
            });
            app.UseSwagger("Microservices Generating Chaos Api V1", "v1");
            SeedData(app);
        }

        private void SeedData(IApplicationBuilder applicationBuilder)
        {
            var citySeedFile = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().FullName), "Seed", "CitySeeder.json");
            if (File.Exists(citySeedFile))
            {
                var cityRepository = applicationBuilder.ApplicationServices.GetRequiredService<ICityRepository>();
                var cities = JsonConvert.DeserializeObject<List<City>>(File.ReadAllText(citySeedFile, System.Text.Encoding.UTF7));
                cityRepository.AddManyAsync(cities).Wait();
            }
        }
    }
}
