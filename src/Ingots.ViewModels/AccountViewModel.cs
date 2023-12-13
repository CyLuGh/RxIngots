using Ingots.Core;
using LanguageExt;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Ingots.ViewModels;

public class AccountViewModel : ReactiveObject
{
    [Reactive] public Option<int> Id { get; private set; }
    [Reactive] public string? Bank { get; set; }
    [Reactive] public string? Bic { get; set; }
    [Reactive] public string? Description { get; set; }
    [Reactive] public string? Iban { get; set; }
    [Reactive] public AccountKind Kind { get; set; }
    [Reactive] public double StartValue { get; set; }
    [Reactive] public string? Stash { get; set; }

    public AccountViewModel()
    {
    }

    public AccountViewModel( Account account )
    {
        Id = account.AccountId;
        Bank = account.Bank;
        Bic = account.Bic;
        Description = account.Description;
        Iban = account.Iban;
        Kind = account.Kind;
        StartValue = account.StartValue;
        Stash = account.Stash;
    }
}