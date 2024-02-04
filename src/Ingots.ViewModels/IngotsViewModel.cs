global using RxUnit = System.Reactive.Unit;
global using RxCommand = ReactiveUI.ReactiveCommand<System.Reactive.Unit,System.Reactive.Unit>;
global using RxInteraction = ReactiveUI.Interaction<System.Reactive.Unit,System.Reactive.Unit>;
using Ingots.Core;
using Ingots.Data;
using Ingots.Parsers;
using Ingots.Parsers.Importers;
using LanguageExt;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace Ingots.ViewModels;

public class IngotsViewModel : ViewModelBase
{
    public Seq<AccountViewModel> Accounts { [ObservableAsProperty] get; }
    
    public ReactiveCommand<AccountViewModel,RxUnit>? EditAccount { get; private set; }
    public Interaction<AccountViewModel,RxUnit> EditAccountInteraction { get; } = new(RxApp.MainThreadScheduler);
    
    public RxCommand? CreateDatabase { get; private set; }
    public RxCommand? TestImporter { get; private set; }
    
    public IngotsViewModel(IDataManager dataManager)
    {
        EditAccountInteraction.RegisterHandler( ctx => ctx.SetOutput( RxUnit.Default ) );
        
        InitializeCommands(dataManager);
        
        this.WhenActivated( disposables =>
        {
            // Observable.Return( RxUnit.Default )
            //     .InvokeCommand( CreateDatabase )
            //     .DisposeWith( disposables );
            
            Observable.Return( RxUnit.Default )
                .InvokeCommand( TestImporter )
                .DisposeWith( disposables );
        } );
    }

    private void InitializeCommands(IDataManager dataManager)
    {
        CreateDatabase = ReactiveCommand.CreateFromTask( async () =>
        {
            await dataManager.CreateDatabaseAsync();
        } );

        TestImporter = ReactiveCommand.CreateRunInBackground(  () =>
        {
            // var ai = new AxaImporter();
            // var axa = ai.Parse(
            //     @"C:\Users\funes\OneDrive\Documents\Extraits\Axa\Historique_BE97750701464049_01012021_31122021_202402021009.csv" )
            //     .ToSeq().Strict();
            //
            // var ki = new KeytradeImporter();
            // var keytrade = ki.Parse( @"C:\Users\funes\OneDrive\Documents\Extraits\Keytrade\BE57 6511 5304 9535_2021.csv" )
            //     .ToSeq().Strict();
            //
            // var fi = new FintroImporter();
            // var fintro = fi.Parse( @"C:\Users\funes\OneDrive\Documents\Extraits\Fintro\CSV_BE23 1448 6046 8791.csv" )
            //     .ToSeq().Strict();

            var ri = new RabobankImporter();
            var rabobank = ri
                .Parse(
                    @"C:\Users\funes\OneDrive\Documents\Extraits\Rabobank\Opérations_14012022_BE87844050049094.csv" )
                .ToSeq().Strict();
            
            Console.WriteLine();
        } );

        EditAccount = ReactiveCommand.CreateFromObservable( (AccountViewModel avm) => EditAccountInteraction.Handle( avm ) );
    }
}