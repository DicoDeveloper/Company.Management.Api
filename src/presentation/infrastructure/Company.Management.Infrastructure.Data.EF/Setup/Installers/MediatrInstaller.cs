using System.Reflection;
using Company.Management.Application.Companies.Queries;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Company.Management.Infrastructure.Data.EF.Setup.Installers;

public static class MediatrInstaller
{
    public static IServiceCollection InstallMediatr(this IServiceCollection services)
        => services.AddMediatR(
                Assembly.GetExecutingAssembly(),
                typeof(FetchCompaniesQuery).Assembly
            );
}