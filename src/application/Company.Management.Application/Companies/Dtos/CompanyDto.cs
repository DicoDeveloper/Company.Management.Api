namespace Company.Management.Application.Companies.Dtos;

public record CompanyDto(
    Guid Id,
    string Name,
    string SizeType
);