namespace Ingots.Core;

public record Transfer : Operation, IEquatable<Transfer>, IComparer<Transfer>
{
    public long TargetId { get; init; }
    public bool IsDerived { get; init; }

    public virtual bool Equals(Transfer? other)
    {
        if (other != null)
            return Id.Equals(other.Id) 
                   && AccountId.Equals(other.AccountId) 
                   && TargetId.Equals(other.TargetId) 
                   && IsDerived.Equals(other.IsDerived);

        return false;
    }

    public override int GetHashCode() => HashCode.Combine(Id, AccountId, TargetId, IsDerived);

    public int Compare(Transfer? x, Transfer? y)
    {
        if (ReferenceEquals(x, y)) return 0;
        if (ReferenceEquals(null, y)) return 1;
        if (ReferenceEquals(null, x)) return -1;
        int accountIdComparison = x.AccountId.CompareTo(y.AccountId);
        if (accountIdComparison != 0) return accountIdComparison;
        int dateComparison = x.Date.CompareTo(y.Date);
        if (dateComparison != 0) return dateComparison;
        int valueComparison = x.Value.CompareTo(y.Value);
        if (valueComparison != 0) return valueComparison;
        int targetIdComparison = x.TargetId.CompareTo(y.TargetId);
        if (targetIdComparison != 0) return targetIdComparison;
        return x.IsDerived.CompareTo(y.IsDerived);
    }
}