using Company.Management.Application.Companies.Interfaces.Projections;
using Company.Management.Infrastructure.Data.EF.Projections;
using Microsoft.Extensions.DependencyInjection;

namespace Company.Management.Infrastructure.Data.EF.Setup.Installers;

public static class ProjectionsInstaller
{
    public static IServiceCollection InstallProjections(this IServiceCollection services)
        => services.AddScoped<ICompanyCreatedProjectionHandler, CompanyCreatedProjectionHandler>()
                    .AddScoped<ICompanyDeletedProjectionHandler, CompanyDeletedProjectionHandler>()
                    .AddScoped<ICompanyUpdatedProjectionHandler, CompanyUpdatedProjectionHandler>();
}