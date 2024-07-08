using Company.Management.Application.Companies.Dtos;
using Company.Management.Domain.Companies.Entities;

namespace Company.Management.Application.Companies.Extensions;

public static class MongoCompanyExtensions
{
    public static CompanyDto ToCompanyDto(this MongoCompany setting)
        => new(
            setting.Id,
            setting.Name ?? "",
            setting.SizeType ?? ""
        );
}
