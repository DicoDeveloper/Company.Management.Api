using Company.Management.Application.Companies.Dtos;
using Company.Management.Application.Core.Messages;
using Company.Management.Application.Shared;

namespace Company.Management.Application.Companies.Response;

public class CompanyResponse : BasePaginatedResponse<CompanyDto>, IQueryResult
{
    public CompanyResponse(
        IEnumerable<CompanyDto> data,
        int currentPage,
        int totalItems,
        int totalPages
    ) : base(data, currentPage, totalItems, totalPages) => Data = data;
}