using Company.Management.Application.Companies.Dtos;
using Company.Management.Domain.Core.Extensions;

namespace Company.Management.Application.Companies.Extensions;

public static class CompanyExtensions
{
    public static CompanyDto ToCompanyDto(this Domain.Companies.Entities.Company setting)
        => new(
            setting.Id,
            setting.Name ?? "",
            setting.SizeType?.GetDescription() ?? ""
        );
}
