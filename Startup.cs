using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using ef_core_5.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ef_core_5
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<SchoolContext>();
            services.AddControllers();
            services.AddSchoolServices();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, SchoolContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                context.SetupDevelopmentDatabase();
            }
            else
            {
                app.UseHttpsRedirection();
            }


            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapControllers();
            });
        }
    }

    public static class ServiceScanner
    {
        public static IServiceCollection AddSchoolServices(this IServiceCollection services)
        {
            const string servicesNamespace = "ef_core_5.Services";
            
            var scannedServices = Assembly
                .GetAssembly(typeof(Startup))
                .GetTypes()
                .Where(t => t.IsClass)
                .Where(t => t.Namespace != null)
                .Where(t => t.Namespace.Contains(servicesNamespace))
                .Where(t => t.Name.EndsWith("Service"));

            Console.WriteLine($"Found {scannedServices.Count()} services");
            
            foreach (var service in scannedServices)
            {
                var interfaceType = service.GetInterfaces().First();
                services.AddTransient(interfaceType, service);
                Console.WriteLine($"Adding service: {interfaceType.Name} - {service.Name}");
            }
 
            return services;
        }
    }
}