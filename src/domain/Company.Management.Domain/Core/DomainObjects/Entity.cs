namespace Company.Management.Domain.Core.DomainObjects;

public abstract class Entity
{
    public Guid Id { get; protected set; }

    public bool IsDeleted { get; set; }

    protected Entity() =>
        Id = Guid.NewGuid();

    protected Entity(Guid id) =>
        Id = id;

    public override bool Equals(object? obj)
    {
        var compareTo = obj as Entity;

        if (ReferenceEquals(this, compareTo))
            return true;

        if (ReferenceEquals(null, compareTo))
            return false;

        return Id.Equals(compareTo.Id);
    }

#pragma warning disable S3875 // "operator==" should not be overloaded on reference types
    public static bool operator ==(Entity? a, Entity? b)
#pragma warning restore S3875 // "operator==" should not be overloaded on reference types
    {
        if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
            return true;

        if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
            return false;

        return a.Equals(b);
    }

    public static bool operator !=(Entity? a, Entity? b) =>
        !(a == b);

    public override int GetHashCode() =>
        GetType().GetHashCode() * 907 + Id.GetHashCode();

    public override string ToString() =>
        $"{GetType().Name} [Id={Id}]";

    public virtual void SetIsDeleted(bool isDeleted)
        => IsDeleted = isDeleted;
}