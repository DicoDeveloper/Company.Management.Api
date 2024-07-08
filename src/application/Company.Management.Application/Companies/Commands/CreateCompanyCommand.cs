using Company.Management.Application.Companies.Dtos;
using Company.Management.Application.Companies.Extensions;
using Company.Management.Application.Companies.Interfaces;
using Company.Management.Domain.Companies.Enums;
using Company.Management.Domain.Core.Extensions;
using MediatR;
using Newtonsoft.Json;

namespace Company.Management.Application.Companies.Commands;

public record CreateCompanyCommand(string Name, string SizeType) : IRequest<CompanyDto>
{
    public Domain.Companies.Entities.Company ToEntity()
        => new(
            Name,
            SizeType.StringToEnum<CompanySizeType>()
        );

    public bool ValidateInput()
        => !string.IsNullOrWhiteSpace(Name) &&
           !string.IsNullOrWhiteSpace(SizeType);
}

public class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyCommand, CompanyDto>
{
    private readonly ICompanyRepository _companyRepository;

    public CreateCompanyCommandHandler(ICompanyRepository companyRepository)
        => _companyRepository = companyRepository;

    public async Task<CompanyDto> Handle(CreateCompanyCommand command, CancellationToken cancellationToken)
    {
        if (!command.ValidateInput())
            throw new ArgumentException($"A requisição possui dados inválidos, CreateCompanyCommand:{JsonConvert.SerializeObject(command)}");

        var entity = command.ToEntity();

        var company = await _companyRepository.CreateAsync(
            entity,
            cancellationToken
        );

        return company.ToCompanyDto();
    }
}