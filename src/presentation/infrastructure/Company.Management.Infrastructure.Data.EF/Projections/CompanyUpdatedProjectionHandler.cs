using Company.Management.Application.Companies.Events;
using Company.Management.Application.Companies.Interfaces;
using Company.Management.Application.Companies.Interfaces.Projections;

namespace Company.Management.Infrastructure.Data.EF.Projections;

public class CompanyUpdatedProjectionHandler(ICompanyMongoRepository companyMongoRepository) : ICompanyUpdatedProjectionHandler
{
    private readonly ICompanyMongoRepository _companyMongoRepository = companyMongoRepository;

    public async Task Handle(CompanyUpdatedEvent @event)
        => await _companyMongoRepository.UpdateAsync(@event.ToEntity());
}