using Company.Management.Domain.Companies.Entities;
using Company.Management.Domain.Core.DomainObjects;

namespace Company.Management.Application.Companies.Events;

public record CompanyCreatedEvent(
    Guid Id,
    string Name,
    string SizeType
) : IEvent
{
    public MongoCompany ToEntity()
        => new(
            Id,
            Name,
            SizeType
        );
}