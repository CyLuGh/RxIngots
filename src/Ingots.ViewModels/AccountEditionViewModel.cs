using Ingots.Data;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace Ingots.ViewModels;

public class AccountEditionViewModel : ViewModelBase
{
    public IngotsViewModel IngotsViewModel { get; }
    private IDataManager DataManager { get; }
    [Reactive] public AccountViewModel? Account { get; set; }
    [Reactive] public bool RequireValidIban { get; set; } = true;

    public ReactiveCommand<AccountViewModel , RxUnit>? AddAccount { get; private set; }

    public RxInteraction CloseDialogInteraction { get; } = new(RxApp.MainThreadScheduler);
    public RxCommand CloseDialog { get; }

    public AccountEditionViewModel( IngotsViewModel ingotsViewModel , IDataManager dataManager )
    {
        IngotsViewModel = ingotsViewModel;
        DataManager = dataManager;

        CloseDialogInteraction.RegisterHandler( ctx => ctx.SetOutput( RxUnit.Default ) );
        CloseDialog = ReactiveCommand.CreateFromObservable( () => CloseDialogInteraction.Handle( RxUnit.Default ) );
        InitializeCommands();

        this.WhenActivated( disposables => { AddAccount!.InvokeCommand( CloseDialog ).DisposeWith( disposables ); } );
    }

    private void InitializeCommands()
    {
        var canAddAccount = this.WhenAnyValue( x => x.RequireValidIban )
            .CombineLatest( this.WhenAnyValue( x => x.Account )
                .WhereNotNull()
                .Select( a => a.WhenAnyValue( x => x.HasValidIban ) )
                .Switch()
                .Merge( this.WhenAnyValue( x => x.Account ).Where( x => x is null ).Select( _ => false ) ) )
            .Select( t =>
            {
                ( bool hasCheck , bool isValid ) = t;
                return !hasCheck || isValid;
            } );
        AddAccount = ReactiveCommand.CreateFromTask(
            async ( AccountViewModel avm ) => { await DataManager.AddAccountAsync( avm.ToAccount() ); } ,
            canAddAccount );
    }
}