using Microsoft.Extensions.DependencyInjection;
using MyShuttle.Data;

namespace MyShuttle.Web.AppBuilderExtensions
{
	public static class DependenciesExtensions
	{
		public static IServiceCollection ConfigureDependencies(this IServiceCollection services)
		{
			services.AddScoped<ICarrierRepository, CarrierRepository>();
			services.AddScoped<IDriverRepository, DriverRepository>();
			services.AddScoped<IVehicleRepository, VehicleRepository>();
			services.AddScoped<IRidesRepository, RidesRepository>();
			return services;
		}
	}
}
