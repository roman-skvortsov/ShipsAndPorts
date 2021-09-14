using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShipsAndPorts.Core.Services;
using ShipsAndPorts.Services.Services;

namespace ShipsAndPorts.Services.Configuration
{
    public static class ModuleInitializer
    {
		public static IServiceCollection ConfigureServices(this IServiceCollection services)
		{
			AddDependenciesToContainer(services);
			return services;
		}

		
		public static void AddDependenciesToContainer(IServiceCollection services)
		{
			services.AddTransient<IShipService, ShipService>();
			services.AddTransient<IPortService, PortService>();
		}
	}
}
