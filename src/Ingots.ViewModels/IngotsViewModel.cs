global using RxUnit = System.Reactive.Unit;
global using RxCommand = ReactiveUI.ReactiveCommand<System.Reactive.Unit,System.Reactive.Unit>;

using Ingots.Data;
using LanguageExt;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace Ingots.ViewModels;

public class IngotsViewModel : ViewModelBase
{
    public Seq<AccountViewModel> Accounts { [ObservableAsProperty] get; }
    
    public RxCommand? CreateDatabase { get; private set; }
    
    public IngotsViewModel(IDataManager dataManager)
    {
        InitializeCommands(dataManager);
        
        this.WhenActivated( disposables =>
        {
            Observable.Return( RxUnit.Default )
                .InvokeCommand( CreateDatabase )
                .DisposeWith( disposables );
        } );
    }

    private void InitializeCommands(IDataManager dataManager)
    {
        CreateDatabase = ReactiveCommand.CreateFromTask( async () =>
        {
            await dataManager.CreateDatabaseAsync();
        } );
    }
}