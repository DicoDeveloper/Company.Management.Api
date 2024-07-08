using Microsoft.Extensions.DependencyInjection;

namespace Company.Management.Infrastructure.Data.EF.Setup.Installers;

public static class MongoDbContextInstaller
{
    public static IServiceCollection InstallMongoDb(this IServiceCollection services, string connectionString)
        => services.AddSingleton(_ => new MongoDbContext(connectionString));
}