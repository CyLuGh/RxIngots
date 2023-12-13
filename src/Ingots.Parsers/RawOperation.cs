namespace Ingots.Parsers;

public readonly record struct RawOperation
{
    public required string Account { get; init; }
    public required double Value { get; init; }
    public required DateTime Date { get; init; }
    public string? Counterpart { get; init; }
    public string? Description { get; init; }
}