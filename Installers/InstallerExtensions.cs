using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationWithDocker.Installers
{
    public static class InstallerExtensions
    {
        public static void InstallerServicesInAssembly(
            this IServiceCollection services,
            IConfiguration configuration
            )
        {
            var installers = typeof(Startup).Assembly.ExportedTypes.Where(x =>
                    typeof(IInstallerServices).IsAssignableFrom(x) &&
                    !x.IsInterface &&
                    !x.IsAbstract
                ).Select(Activator.CreateInstance)
                .Cast<IInstallerServices>()
                .ToList();
            installers.ForEach(installers => installers.InstallServices(configuration, services));
        }
    }
}
