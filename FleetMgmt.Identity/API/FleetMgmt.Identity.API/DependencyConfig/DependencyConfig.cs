using FleetMgmt.Identity.Domain.Dto;
using FleetMgmt.Identity.Domain.Interfaces;
using FleetMgmt.Identity.Domain.Validators;
using FleetMgmt.Identity.Infrastructure.Repositories;
using FleetMgmt.Identity.Interfaces;
using FleetMgmt.Identity.Services;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SD.BuildingBlocks.Infrastructure;
using SD.BuildingBlocks.Repository;

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
            _services.AddScoped<ILoginService, LoginService>();
            _services.AddScoped<IRegistrationService, RegistrationService>();
        }

        private void InjectRepositories()
        {
            _services.AddScoped<ICompanyRepository, CompanyRepository>();
            _services.AddScoped<IGroupsOuRepository, GroupsOuRepository>();
            _services.AddScoped<IGroupsRepository, GroupsRepository>();
            _services.AddScoped<IOuRepository, OuRepository>();
            _services.AddScoped<ITokenControllerRepository, TokenControllerRepository>();
            _services.AddScoped<IUserMetadataRepository, UserMetadataRepository>();
            _services.AddScoped<IUserRepository, UserRepository>();
            _services.AddScoped<IUsersGroupsRepository, UsersGroupsRepository>();
            _services.AddScoped<ITemplateSettingRepository, TemplateSettingRepository>();
        }

        private void InjectUtilities()
        {
            _services.AddScoped(typeof(IRepository<>), typeof(RepositoryEF<>));
            _services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            _services.AddScoped(typeof(ITransactionalUnitOfWork), typeof(TransactionalUnitOfWork));
        }

        private void InjectValidators()
        {
            _services.AddScoped<IValidator<UserRegistrationRequestDto>, UserRegistrationRequestValidator>();
        }
    }
}
