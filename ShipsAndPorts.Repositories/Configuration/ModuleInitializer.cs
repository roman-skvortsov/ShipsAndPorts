using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShipsAndPorts.Core.Repositories;
using ShipsAndPorts.Repositories.DatabaseContext;
using ShipsAndPorts.Repositories.Repositories;

namespace ShipsAndPorts.Repositories.Configuration
{
    public static class ModuleInitializer
    {
		public static IServiceCollection ConfigureRepositories(this IServiceCollection services, IConfiguration configuration)
		{
			SetSettings(services, configuration);
			AddDependenciesToContainer(services);

			return services;
		}

        private static void SetSettings(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ShipsAndPortsDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("ShipsAndPortsDb"));
            });
        }

        private static void AddDependenciesToContainer(IServiceCollection services)
		{
			services.AddTransient<IShipRepository, ShipRepository>();
			services.AddTransient<IPortRepository, PortRepository>();
		}
	}
}
