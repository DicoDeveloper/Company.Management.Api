using Company.Management.Application.Companies.Interfaces;
using MediatR;

namespace Company.Management.Application.Companies.Commands;

public record DeleteCompanyCommand(Guid Id) : IRequest<Unit>
{ }

public class DeleteCompanyCommandHandler : IRequestHandler<DeleteCompanyCommand, Unit>
{
    private readonly ICompanyRepository _companyRepository;

    public DeleteCompanyCommandHandler(ICompanyRepository companyRepository)
        => _companyRepository = companyRepository;

    public async Task<Unit> Handle(DeleteCompanyCommand request, CancellationToken cancellationToken)
    {
        var company = await _companyRepository.GetByIdAsync(
            request.Id,
            cancellationToken
        );

        company.SetIsDeleted(true);

        await _companyRepository.UnitOfWork.CommitAsync();

        return Unit.Value;
    }
}