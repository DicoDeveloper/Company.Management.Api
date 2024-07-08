using Company.Management.Application.Core.Data;
using Company.Management.Domain.Core.DomainObjects;

namespace Company.Management.Application.Interfaces;

public interface IRepository<T> where T : IEntity
{
    IUnitOfWork UnitOfWork { get; }

    Task<T> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default
    );

    Task<T> CreateAsync(
        T entity,
        CancellationToken cancellationToken = default
    );

    Task RemoveAsync(
        Guid id,
        CancellationToken cancellationToken = default
    );
}
