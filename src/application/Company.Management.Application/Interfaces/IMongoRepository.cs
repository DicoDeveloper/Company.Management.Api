namespace Company.Management.Application.Interfaces;

public interface IMongoRepository<T>
{
    Task<T> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default
    );

    Task CreateAsync(
        T entity
    );

    Task UpdateAsync(
        T entity,
        CancellationToken cancellationToken = default
    );

    Task RemoveAsync(
        Guid id,
        CancellationToken cancellationToken = default
    );
}
