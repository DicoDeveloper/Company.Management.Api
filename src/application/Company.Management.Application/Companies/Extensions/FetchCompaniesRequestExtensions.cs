using Company.Management.Application.Companies.Queries;
using Company.Management.Application.Companies.Requests;

namespace Company.Management.Application.Companies.Extensions;

public static class FetchCompaniesRequestExtensions
{
    public static FetchCompaniesQuery ToFetchCompaniesQuery(
        this FetchCompaniesRequest request
    )
        => new()
        {
            CompanyName = request.CompanyName,
            CompanySizeType = request.CompanySizeType,
            Page = request.Page,
            Size = request.Size
        };
}