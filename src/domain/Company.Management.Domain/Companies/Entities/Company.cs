using Company.Management.Domain.Companies.Enums;
using Company.Management.Domain.Core.DomainObjects;

namespace Company.Management.Domain.Companies.Entities;

public class Company : Entity, IEntity
{
    public string? Name { get; private set; }
    public CompanySizeType? SizeType { get; private set; }

#pragma warning disable CS8618
    private Company() { }
#pragma warning restore CS8618

    public Company(
        string name,
        CompanySizeType sizeType
    )
    {
        Name = name;
        SizeType = sizeType;
    }

    public void SetName(string? name)
        => Name = name;

    public void SetSizeType(CompanySizeType? sizeType)
        => SizeType = sizeType;
}