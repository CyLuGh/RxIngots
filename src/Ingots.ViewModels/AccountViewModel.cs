using Ingots.Core;
using LanguageExt;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace Ingots.ViewModels;

public class AccountViewModel : ViewModelBase
{
    [Reactive] public Option<int> Id { get; private set; }
    [Reactive] public string? Bank { get; set; }
    [Reactive] public string? Bic { get; set; }
    [Reactive] public string? Description { get; set; }
    [Reactive] public string? Iban { get; set; }
    [Reactive] public AccountKind Kind { get; set; }
    [Reactive] public decimal StartValue { get; set; }
    [Reactive] public string? Stash { get; set; }
    [Reactive] public Seq<Owner> Owners { get; set; }
    
    public bool HasValidIban { [ObservableAsProperty] get; }

    public AccountViewModel()
    {
        this.WhenActivated( disposables =>
        {
            this.WhenAnyValue( x => x.Iban )
                .Select( IbanUtils.IsValid )
                .ToPropertyEx( this , x => x.HasValidIban , scheduler: RxApp.MainThreadScheduler )
                .DisposeWith( disposables );
        } );
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
        Owners = account.Owners.ToSeq();
    }

    public Account ToAccount() =>
        new()
        {
            AccountId = Id.Match( i => i , () => -1 ) ,
            Bank = Bank ?? string.Empty,
            Bic = Bic ?? string.Empty,
            Description = Description ?? string.Empty,
            Iban = Iban ?? string.Empty,
            Kind = Kind ,
            StartValue = StartValue ,
            Stash = Stash ?? string.Empty,
            Owners = Owners.ToArray()
        };
}