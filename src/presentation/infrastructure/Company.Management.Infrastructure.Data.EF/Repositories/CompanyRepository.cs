using Company.Management.Application.Companies.Interfaces;

namespace Company.Management.Infrastructure.Data.EF.Repositories;

public class CompanyRepository(Context context) : Repository<Domain.Companies.Entities.Company>(context), ICompanyRepository
{ }