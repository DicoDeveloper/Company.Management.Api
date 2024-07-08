using Company.Management.Application.Interfaces;

namespace Company.Management.Application.Companies.Interfaces;

public interface ICompanyRepository : IRepository<Domain.Companies.Entities.Company>
{ }
