using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pogodeo.DataAccess;
using Pogodeo.Services;
using Pogodeo.Services.ExternalApiServices;

namespace Pogodeo
{
    public class Startup
    {
        public readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //TEST
            services.AddMvc();
            services.AddDbContext<PogodeoAppDataContext>(options =>
            {
                options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped<IForecastRepository, ForecastRepository>();
            services.AddScoped<ITestService, TestService>();
            services.AddScoped<IOpenCageGeocoder, OpenCageGeocoder>();
            services.AddScoped<IGeolocationService, GeolocationService>();


        }
        
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=ProvideData}/{action=Index}/{id?}");
            });

            InitializeDatabase(app);
        }
         
        private void InitializeDatabase(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                scope.ServiceProvider.GetRequiredService<PogodeoAppDataContext>().Database.Migrate();
            }
        }
    }
}
