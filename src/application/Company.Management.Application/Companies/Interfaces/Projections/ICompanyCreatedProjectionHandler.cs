using Company.Management.Application.Companies.Events;
using Company.Management.Application.Interfaces;

namespace Company.Management.Application.Companies.Interfaces.Projections;

public interface ICompanyCreatedProjectionHandler : IProjectionBaseHandler<CompanyCreatedEvent>
{ }