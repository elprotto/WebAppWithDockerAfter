using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace WebApplicationWithDocker.Installers
{
    public class MvcInstaller : IInstallerServices
    {
        public void InstallServices(
            IConfiguration Configuration, 
            IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
            });
        }
    }
}
