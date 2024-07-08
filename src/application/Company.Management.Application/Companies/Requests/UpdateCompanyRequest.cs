using Microsoft.AspNetCore.Mvc;

namespace Company.Management.Application.Companies.Requests;

public record UpdateCompanyRequest
{
    [FromQuery(Name = "name")]
    public string? Name { get; set; }

    [FromQuery(Name = "sizeType")]
    public string? SizeType { get; set; }
}