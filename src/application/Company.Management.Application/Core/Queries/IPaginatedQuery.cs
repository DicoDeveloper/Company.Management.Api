namespace Company.Management.Application.Core.Queries;

public interface IPaginatedQuery<out T> : IQuery<T>
{
    int Page { get; }
    int Size { get; }
}