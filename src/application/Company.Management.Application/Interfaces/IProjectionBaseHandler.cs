using Company.Management.Domain.Core.DomainObjects;

namespace Company.Management.Application.Interfaces;

public interface IProjectionBaseHandler<TEvent> where TEvent : IEvent
{
    Task Handle(TEvent @event);
}