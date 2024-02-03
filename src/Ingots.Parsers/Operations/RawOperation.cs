namespace Ingots.Parsers.Operations;

public readonly record struct RawOperation
{
    public required decimal Value { get; init; }
    public required DateTime Date { get; init; }
    public string? Counterpart { get; init; }
    public string? Description { get; init; }
}