namespace Ingots.Core;

public record Transaction : Operation, IEquatable<Transaction>, IComparer<Transaction>
{
    public string? Category { get; set; }
    public string? SubCategory { get; set; }
    public string? Shop { get; set; }

    public virtual bool Equals(Transaction? other)
    {
        if (other is not null)
            return Id.Equals(other.Id) && AccountId.Equals(other.AccountId);

        return false;
    }

    public override int GetHashCode() => HashCode.Combine(Id, AccountId);

    public int Compare(Transaction? x, Transaction? y)
    {
        if (ReferenceEquals(x, y)) return 0;
        if (ReferenceEquals(null, y)) return 1;
        if (ReferenceEquals(null, x)) return -1;
        int accountIdComparison = x.AccountId.CompareTo(y.AccountId);
        if (accountIdComparison != 0) return accountIdComparison;
        int dateComparison = x.Date.CompareTo(y.Date);
        if (dateComparison != 0) return dateComparison;
        return x.Value.CompareTo(y.Value);
    }
}