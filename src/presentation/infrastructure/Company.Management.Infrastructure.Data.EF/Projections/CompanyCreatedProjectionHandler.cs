using Company.Management.Application.Companies.Events;
using Company.Management.Application.Companies.Interfaces;
using Company.Management.Application.Companies.Interfaces.Projections;

namespace Company.Management.Infrastructure.Data.EF.Projections;

public class CompanyCreatedProjectionHandler(ICompanyMongoRepository companyMongoRepository) : ICompanyCreatedProjectionHandler
{
    private readonly ICompanyMongoRepository _companyMongoRepository = companyMongoRepository;

    public async Task Handle(CompanyCreatedEvent @event)
        => await _companyMongoRepository.CreateAsync(@event.ToEntity());
}