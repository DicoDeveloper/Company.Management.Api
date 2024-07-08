using Company.Management.Application.Core.Data;
using Microsoft.EntityFrameworkCore;

namespace Company.Management.Infrastructure.Data.EF;

public class Context : DbContext, IUnitOfWork
{
    public Context(DbContextOptions options) : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var assembly = typeof(Context).Assembly;
        modelBuilder.ApplyConfigurationsFromAssembly(assembly);

        base.OnModelCreating(modelBuilder);
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        => base.ConfigureConventions(configurationBuilder);

    public async Task<bool> CommitAsync()
        => await SaveChangesAsync() > 0;

    public DbSet<Domain.Companies.Entities.Company> Companies { get; set; }
}