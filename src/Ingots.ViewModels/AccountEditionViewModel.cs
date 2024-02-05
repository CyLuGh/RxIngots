using Ingots.Data;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Reactive.Disposables;

namespace Ingots.ViewModels;

public class AccountEditionViewModel : ViewModelBase
{
    public IngotsViewModel IngotsViewModel { get; }
    public IDataManager DataManager { get; }
    [Reactive] public AccountViewModel? Account { get; set; }
    
    public ReactiveCommand<AccountViewModel,RxUnit>? AddAccount { get; private set; }

    public RxInteraction CloseDialogInteraction { get; } = new(RxApp.MainThreadScheduler);
    public RxCommand CloseDialog { get; }

    public AccountEditionViewModel(IngotsViewModel ingotsViewModel, IDataManager dataManager)
    {
        IngotsViewModel = ingotsViewModel;
        DataManager = dataManager;

        CloseDialogInteraction.RegisterHandler( ctx => ctx.SetOutput( RxUnit.Default ) );
        CloseDialog = ReactiveCommand.CreateFromObservable( () => CloseDialogInteraction.Handle( RxUnit.Default ) );
        InitializeCommands();
        
        this.WhenActivated( disposables =>
        {
            AddAccount.InvokeCommand( CloseDialog )
                .DisposeWith( disposables );
        } );
    }

    private void InitializeCommands()
    {
        AddAccount = ReactiveCommand.CreateFromTask( async ( AccountViewModel avm ) =>
        {
            await DataManager.AddAccountAsync( avm.ToAccount() );
        } );
    }
}