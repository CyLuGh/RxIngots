namespace Ingots.Core;

public abstract record Operation
{
    public long Id { get; init; }
    public int AccountId { get; init; }
    public DateTimeOffset Date { get; init; }
    public double Value { get; init; }
    public string? Description { get; init; }
    public bool IsExecuted { get; init; }
}