namespace Company.Management.Application.Core.Data;

public interface IUnitOfWork
{
    Task<bool> CommitAsync();
}