using MediatR;

namespace Company.Management.Application.Core.Queries;

public interface IQuery<out IQueryResult> : IRequest<IQueryResult>
{ }