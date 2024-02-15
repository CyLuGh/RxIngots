namespace Ingots.Core;

public record Account
{
    public int AccountId { get; init; } = -1;
    public string Bank { get; init; } = string.Empty;
    public string Bic { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public required string Iban { get; init; } = string.Empty;
    public AccountKind Kind { get; init; } = AccountKind.Checking;
    public decimal StartValue { get; init; }
    public string Stash { get; init; } = string.Empty;
    public Owner[] Owners { get; init; } = [];
}