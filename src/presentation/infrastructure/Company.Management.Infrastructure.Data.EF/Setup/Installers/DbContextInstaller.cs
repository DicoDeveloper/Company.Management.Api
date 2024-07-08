using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Company.Management.Infrastructure.Data.EF.Setup.Installers;

public static class DbContextInstaller
{
    public static IServiceCollection InstallDbContext(this IServiceCollection services, string connectionString)
        => services.AddDbContext<Context>(
            options => options.UseSqlServer(connectionString)
        );
}