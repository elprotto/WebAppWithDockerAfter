using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationWithDocker.Data.Migrations;
using WebApplicationWithDocker.Services;

namespace WebApplicationWithDocker.Installers
{
    public class DbInstaller : IInstallerServices
    {
        public void InstallServices(
            IConfiguration Configuration, 
            IServiceCollection services)
        {
            services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection"))
                );
            //services.AddDefaultIdentity<IdentityUser>();
            services.AddSingleton<IPostService, PostService>();
        }
    }
}
