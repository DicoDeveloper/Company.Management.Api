using Company.Management.Domain.Core.DomainObjects;

namespace Company.Management.Application.Companies.Events;

public record CompanyDeletedEvent(Guid Id) : IEvent;