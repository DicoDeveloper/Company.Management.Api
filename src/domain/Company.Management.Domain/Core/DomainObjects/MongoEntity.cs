namespace Company.Management.Domain.Core.DomainObjects;

public abstract class MongoEntity
{
    public Guid Id { get; set; }

    public bool IsDeleted { get; set; }

    protected MongoEntity(Guid id) =>
        Id = id;

    public override int GetHashCode() =>
        GetType().GetHashCode() * 907 + Id.GetHashCode();

    public override string ToString() =>
        $"{GetType().Name} [Id={Id}]";

    public virtual void SetIsDeleted(bool isDeleted)
        => IsDeleted = isDeleted;
}