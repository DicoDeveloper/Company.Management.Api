using Microsoft.AspNetCore.Mvc;

namespace Company.Management.Application.Companies.Requests;

public record FetchCompaniesRequest
{
    [FromQuery(Name = "companyName")]
    public string? CompanyName { get; set; }

    [FromQuery(Name = "companySizeType")]
    public string? CompanySizeType { get; set; }

    [FromQuery(Name = "_page")]
    public int Page { get; set; } = 0;

    [FromQuery(Name = "_size")]
    public int Size { get; set; } = 10;
}