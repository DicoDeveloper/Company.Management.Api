using Company.Management.Application.Core.Data;
using Company.Management.Application.Interfaces;
using Company.Management.Domain.Core.DomainObjects;
using Company.Management.Infrastructure.Data.EF.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Company.Management.Infrastructure.Data.EF.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, IEntity
{
    protected readonly Context _context;

    public Repository(Context context) => _context = context;

    public IUnitOfWork UnitOfWork => _context;

    public async Task<TEntity> CreateAsync(
        TEntity entity,
        CancellationToken cancellationToken = default
    )
    {
        var newEntity = await _context.Set<TEntity>().AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return newEntity.Entity;
    }

    public async Task RemoveAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entityToRemove = await GetByIdAsync(id, cancellationToken);

        _context.Set<TEntity>().Remove(entityToRemove);

        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<TEntity> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default
    )
    {
        var query = _context.Set<TEntity>()
            .AsQueryable();

        var result = await query.FirstOrDefaultAsync(entity => !entity.IsDeleted && entity.Id == id, cancellationToken);

        return result ?? throw new NotFoundException();
    }
}