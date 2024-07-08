using Company.Management.Application.Companies.Interfaces;
using Company.Management.Infrastructure.Data.EF.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Company.Management.Infrastructure.Data.EF.Setup.Installers;

public static class RepositoryInstaller
{
    public static IServiceCollection InstallRepositories(this IServiceCollection services)
        => services.AddScoped<ICompanyRepository, CompanyRepository>()
            .AddScoped<ICompanyMongoRepository, CompanyMongoRepository>();
}