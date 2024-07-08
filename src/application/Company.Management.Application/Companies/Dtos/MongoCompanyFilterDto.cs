using Company.Management.Application.Core.Pagination;

namespace Company.Management.Application.Companies.Dtos;

public class MongoCompanyFilterDto
{
    public string? Name { get; set; }
    public string? SizeType { get; set; }
    public Paginated? Paginated { get; set; }

    public bool IsValid => Paginated != null && Paginated.IsValid;
}