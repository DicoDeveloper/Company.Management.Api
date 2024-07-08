using Company.Management.Domain.Core.DomainObjects;

namespace Company.Management.Domain.Companies.Entities;

public class MongoCompany : MongoEntity, IEntity
{
    public string? Name { get; private set; }
    public string? SizeType { get; private set; }

    public MongoCompany(
        Guid id,
        string name,
        string sizeType
    ) : base(id)
    {
        Name = name;
        SizeType = sizeType;
    }

    public void SetName(string? name)
        => Name = name;

    public void SetSizeType(string? sizeType)
        => SizeType = sizeType;
}