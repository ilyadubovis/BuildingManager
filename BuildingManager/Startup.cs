using Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Service;

using System.Text.Json;
using System.Text.Json.Serialization;

namespace BuildingManager
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddMvc()
                .AddJsonOptions(o =>
                {
                    o.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                    o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddOptions<DataSettings>().Configure<IConfiguration>((settings, configuration) =>
            {
                IConfigurationSection section = configuration.GetSection("Data");
                section.Bind(settings);
            })
            .ValidateDataAnnotations();

            services.AddScoped<DataContext>();

            services.AddScoped<IBuildingRepo, BuildingRepo>();
            services.AddScoped<IBuildingOwnerRepo, BuildingOwnerRepo>();
            services.AddScoped<IBuildingService, BuildingService>();
            services.AddScoped<IBuildingOwnerService, BuildingOwnerService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
