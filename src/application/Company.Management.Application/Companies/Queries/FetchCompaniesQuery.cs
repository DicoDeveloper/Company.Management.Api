using Company.Management.Application.Companies.Extensions;
using Company.Management.Application.Companies.Interfaces;
using Company.Management.Application.Companies.Response;
using Company.Management.Application.Core.Queries;
using Company.Management.Domain.Companies.Entities;
using MediatR;

namespace Company.Management.Application.Companies.Queries;

public class FetchCompaniesQuery : IPaginatedQuery<CompanyResponse>
{
    public FetchCompaniesQuery() { }

    public string? CompanyName { get; set; }
    public string? CompanySizeType { get; set; }
    public int Page { get; set; } = 0;
    public int Size { get; set; } = 10;
}

public class FetchCompaniesQueryHandler : IRequestHandler<FetchCompaniesQuery, CompanyResponse>
{
    private readonly ICompanyMongoRepository _companyMongoRepository;

    public FetchCompaniesQueryHandler(
        ICompanyMongoRepository companyMongoRepository
    )
        => _companyMongoRepository = companyMongoRepository;

    public async Task<CompanyResponse> Handle(FetchCompaniesQuery request, CancellationToken cancellationToken)
    {
        var (companies, count, totalPages) = await GetDataAndCount(request, cancellationToken);

        var companyDto = companies.Select(company => company.ToCompanyDto());

        return new CompanyResponse(
            companyDto,
            request.Page,
            count,
            totalPages
        );
    }

    private async Task<(IEnumerable<MongoCompany>, int, int)> GetDataAndCount(
        FetchCompaniesQuery request,
        CancellationToken cancellationToken)
    {
        var (count, companies) = await _companyMongoRepository.FetchAsync(
            new()
            {
                Page = request.Page,
                Size = request.Size,
            },
            request.CompanyName,
            request.CompanySizeType,
            cancellationToken
        );

        var totalPages = Math.Ceiling(count / (double)request.Size);

        return (companies, count, (int)totalPages);
    }
}