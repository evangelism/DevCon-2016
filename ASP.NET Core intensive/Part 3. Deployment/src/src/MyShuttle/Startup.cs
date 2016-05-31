using Microsoft.AspNetCore.Builder;
using MyShuttle.Web.AppBuilderExtensions;
using MyShuttle.Data;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyShuttle.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace MyShuttle.Web
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("config.json", optional: true)
                //.AddEnvironmentVariables(prefix: "ASPNETCORE_")
                .Build();

            Configuration = config;
        }
        public IConfiguration Configuration { get; private set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureDataContext(Configuration);

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<MyShuttleContext>()
                .AddDefaultTokenProviders();

            services.ConfigureDependencies();
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.ConfigureRoutes();
            app.UseStaticFiles();
            MyShuttleDataInitializer.InitializeDatabaseAsync(app.ApplicationServices).Wait();
        }

        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseKestrel()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
