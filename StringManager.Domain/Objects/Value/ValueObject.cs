namespace StringManager.Domain.Objects.Value;

public abstract class ValueObject
{
    public override bool Equals(object? obj)
    {
        if (obj == null || obj.GetType() != GetType())
        {
            return false;
        }

        return GetEqualityComponents()
            .SequenceEqual(((ValueObject) obj).GetEqualityComponents());
    }

    public override int GetHashCode() =>
        GetEqualityComponents()
            .Select(x =>
                x != null ? x.GetHashCode() : 0)
            .Aggregate((x, y) => x ^ y);

    public static bool operator ==(ValueObject? left, ValueObject? right) => EqualOperator(left, right);
    public static bool operator !=(ValueObject? left, ValueObject? right) => NotEqualOperator(left, right);
    
    protected abstract IEnumerable<object?> GetEqualityComponents();

    protected static bool EqualOperator(ValueObject? left, ValueObject? right)
    {
        if (ReferenceEquals(left, null) ^ ReferenceEquals(right, null))
        {
            return false;
        }

        return ReferenceEquals(left, null) || left.Equals(right);
    }

    protected static bool NotEqualOperator(ValueObject? left, ValueObject? right)
    {
        return !EqualOperator(left, right);
    }
}