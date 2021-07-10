using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WebApplicationWithDocker.Installers
{
    public interface IInstallerServices
    {
        void InstallServices(
            IConfiguration Configuration,
            IServiceCollection services);
    }
}
