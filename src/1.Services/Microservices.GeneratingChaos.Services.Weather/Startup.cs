using System;
using System.Collections.Generic;
using System.IO;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microservices.GeneratingChaos.BuildingBlocks.Extensions;
using Microservices.GeneratingChaos.BuildingBlocks.Infrastructure.DataBase;
using Microservices.GeneratingChaos.Services.Weather.Domain.Entities;
using Microservices.GeneratingChaos.Services.Weather.Infrastructure.AutofacModules;
using Microservices.GeneratingChaos.Services.Weather.Infrastructure.Repository.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Microservices.GeneratingChaos.Services.Weather
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public virtual IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddCustomMvc<Startup>()
                    .AddSwagger("v1", "Microservices Generating Chaos Weather Service", "Sample weather service for netconf 2019")
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
            app.UseSwagger("Microservices Generating Chaos Weather Service V1", "v1");
            SeedData(app);
        }

        private void SeedData(IApplicationBuilder applicationBuilder)
        {
            var weatherSeedFile = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().FullName), "Seed", "Weather.json");
            if (File.Exists(weatherSeedFile))
            {
                var weatherRepository = applicationBuilder.ApplicationServices.GetRequiredService<IWeatherRepository>();
                var weathers = JsonConvert.DeserializeObject<List<WeatherForecast>>(File.ReadAllText(weatherSeedFile, System.Text.Encoding.UTF7));
                weatherRepository.AddManyAsync(weathers).Wait();
            }
        }
    }
}
