namespace MyShuttle.Web.AppBuilderExtensions
{
    using Data;
    using System;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Infrastructure;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Configuration;

    public static class DataContextExtensions
    {
        public static IServiceCollection ConfigureDataContext(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration == null)
                throw new Exception("sex1");

            var runningOnMono = Type.GetType("Mono.Runtime") != null;
            var configInMemory = configuration["Data:UseInMemoryStore"] != null && configuration["Data:UseInMemoryStore"].Equals("true", StringComparison.OrdinalIgnoreCase);
            bool useInMemoryStore = runningOnMono || configInMemory;

            var connectionStrging = configuration["Data:DefaultConnection:ConnectionString"];
            if (useInMemoryStore || string.IsNullOrEmpty(connectionStrging))
            {
                services.AddEntityFrameworkInMemoryDatabase()
                  .AddDbContext<MyShuttleContext>(options =>
                  {
                      options.UseInMemoryDatabase();
                  });
            }
            else
            {
                services.AddEntityFrameworkSqlServer()
                   .AddDbContext<MyShuttleContext>(options =>
                   {
                       options.UseSqlServer(connectionStrging);
                   });
            }

            return services;
        }
    }
}