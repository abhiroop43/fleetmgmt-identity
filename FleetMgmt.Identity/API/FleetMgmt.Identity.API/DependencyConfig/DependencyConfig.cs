using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SD.BuildingBlocks.Infrastructure;

namespace FleetMgmt.Identity.API.DependencyConfig
{
    public class DependencyConfig
    {
        private readonly IServiceCollection _services;
        private readonly IConfiguration _configuration;

        public DependencyConfig(IServiceCollection services, IConfiguration configuration)
        {
            _services = services;
            _configuration = configuration;
        }

        public void ConfigureServices()
        {
            InjectServices();
            InjectRepositories();
            InjectUtilities();
            InjectValidators();
        }

        private void InjectServices()
        {

        }

        private void InjectRepositories()
        {

        }

        private void InjectUtilities()
        {
            _services.AddScoped(typeof(IRepository<>), typeof(RepositoryEF<>));
            _services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        private void InjectValidators()
        {

        }
    }
}
