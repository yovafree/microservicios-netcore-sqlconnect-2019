using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CatalogsApi
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
            services.AddConsulConfig(Configuration);
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }

    
    public static class AppExtensions  
    {             
        public static IServiceCollection AddConsulConfig(this IServiceCollection services, IConfiguration configuration)  
        {  
            services.AddSingleton<IConsulClient, ConsulClient>(p => new ConsulClient(consulConfig =>  
            {  
                var address = configuration.GetValue<string>("Consul:Host");  
                consulConfig.Address = new Uri(address);  
            }));  
            return services;  
        }

        [Obsolete]
        public static IApplicationBuilder UseConsul(this IApplicationBuilder app)  
        {  
            var consulClient = app.ApplicationServices.GetRequiredService<IConsulClient>();  
            var logger = app.ApplicationServices.GetRequiredService<ILoggerFactory>().CreateLogger("AppExtensions");  
            var lifetime = app.ApplicationServices.GetRequiredService<Microsoft.AspNetCore.Hosting.IApplicationLifetime>();  
      
            if (!(app.Properties["server.Features"] is FeatureCollection features)) return app;  
      
            var addresses = features.Get<IServerAddressesFeature>();  
            var address = addresses.Addresses.First();  
      
            Console.WriteLine($"address={address}");  
      
            var uri = new Uri(address);  
            var registration = new AgentServiceRegistration()  
            {  
                ID = $"catalogs-api-{uri.Port}",  
                // servie name  
                Name = "catalogs-api",  
                Address = $"{uri.Host}",  
                Port = uri.Port  
            };  
      
            logger.LogInformation("Registering with Consul");  
            consulClient.Agent.ServiceDeregister(registration.ID).ConfigureAwait(true);  
            consulClient.Agent.ServiceRegister(registration).ConfigureAwait(true);  
      
            lifetime.ApplicationStopping.Register(() =>  
            {  
                logger.LogInformation("Unregistering from Consul");  
                consulClient.Agent.ServiceDeregister(registration.ID).ConfigureAwait(true);  
            });  
      
            return app;  
        }  
    }  

}
