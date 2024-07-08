using Company.Management.Application.Companies.Events;
using Company.Management.Application.Companies.Interfaces;
using Company.Management.Application.Companies.Interfaces.Projections;

namespace Company.Management.Infrastructure.Data.EF.Projections;

public class CompanyDeletedProjectionHandler(ICompanyMongoRepository companyMongoRepository) : ICompanyDeletedProjectionHandler
{
    private readonly ICompanyMongoRepository _companyMongoRepository = companyMongoRepository;

    public async Task Handle(CompanyDeletedEvent @event)
        => await _companyMongoRepository.RemoveAsync(@event.Id);
}