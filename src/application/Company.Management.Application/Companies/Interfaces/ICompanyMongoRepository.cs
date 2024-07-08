using Company.Management.Application.Core.Pagination;
using Company.Management.Application.Interfaces;
using Company.Management.Domain.Companies.Entities;

namespace Company.Management.Application.Companies.Interfaces;

public interface ICompanyMongoRepository : IMongoRepository<MongoCompany>
{
    Task<(int, IList<MongoCompany>)> FetchAsync(
        Paginated paginated,
        string? name = null,
        string? sizeType = null,
        CancellationToken cancellationToken = default
    );
}
