namespace Ingots.Core;

public record Account
{
    public int AccountId { get; init; }
    public string? Bank { get; init; } = "Unknown";
    public string? Bic { get; init; } = "Unknown";
    public string? Description { get; init; } = "Unknown";
    public string? Iban { get; init; }
    public AccountKind Kind { get; init; }
    public double StartValue { get; init; }
    public string? Stash { get; init; } = "None";
    public Owner[] Owners { get; init; } = [];
}