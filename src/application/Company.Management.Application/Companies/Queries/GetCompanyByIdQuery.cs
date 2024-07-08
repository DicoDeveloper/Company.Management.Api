using Company.Management.Application.Companies.Dtos;
using Company.Management.Application.Companies.Extensions;
using Company.Management.Application.Companies.Interfaces;
using Company.Management.Application.Core.Queries;
using MediatR;

namespace Company.Management.Application.Companies.Queries;

public record GetCompanyByIdQuery(Guid Id) : IQuery<CompanyDto?> { }

public class GetCompanyByIdQueryHandler : IRequestHandler<GetCompanyByIdQuery, CompanyDto?>
{
    private readonly ICompanyMongoRepository _companyMongoRepository;

    public GetCompanyByIdQueryHandler(ICompanyMongoRepository companyMongoRepository)
        => _companyMongoRepository = companyMongoRepository;

    public async Task<CompanyDto?> Handle(GetCompanyByIdQuery request, CancellationToken cancellationToken)
    {
        var company = await _companyMongoRepository.GetByIdAsync(request.Id, cancellationToken);

        return company?.ToCompanyDto();
    }
}