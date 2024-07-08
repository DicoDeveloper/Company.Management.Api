using Company.Management.Application.Companies.Dtos;
using Company.Management.Application.Companies.Extensions;
using Company.Management.Application.Companies.Interfaces;
using Company.Management.Domain.Companies.Enums;
using Company.Management.Domain.Core.Extensions;
using MediatR;
using Newtonsoft.Json;

namespace Company.Management.Application.Companies.Commands;

public record UpdateCompanyCommand(Guid Id, string? Name, string? SizeType) : IRequest<CompanyDto>
{
    public bool ValidateInput()
        => !string.IsNullOrWhiteSpace(Name) &&
           !string.IsNullOrWhiteSpace(SizeType);
}

public class UpdateCompanyCommandHandler : IRequestHandler<UpdateCompanyCommand, CompanyDto>
{
    private readonly ICompanyRepository _companyRepository;

    public UpdateCompanyCommandHandler(ICompanyRepository companyRepository)
        => _companyRepository = companyRepository;

    public async Task<CompanyDto> Handle(UpdateCompanyCommand command, CancellationToken cancellationToken)
    {
        if (!command.ValidateInput())
            throw new ArgumentException($"A requisição possui dados inválidos, UpdateCompanyCommand:{JsonConvert.SerializeObject(command)}");

        var company = await _companyRepository.GetByIdAsync(command.Id, cancellationToken);

        UpdateCompany(command, company);

        await _companyRepository.UnitOfWork.CommitAsync();

        return company.ToCompanyDto();
    }

    private static void UpdateCompany(UpdateCompanyCommand command, Domain.Companies.Entities.Company company)
    {
        company.SetName(command.Name!);
        company.SetSizeType(command.SizeType!.StringToEnum<CompanySizeType>());
    }
}