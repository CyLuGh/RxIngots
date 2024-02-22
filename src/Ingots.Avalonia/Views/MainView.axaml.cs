using Avalonia.Controls;
using Avalonia.Platform.Storage;
using Avalonia.ReactiveUI;
using Ingots.ViewModels;
using ReactiveUI;
using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace Ingots.Avalonia.Views;

public partial class MainView : ReactiveUserControl<IngotsViewModel>
{
    public MainView()
    {
        InitializeComponent();

        this.WhenActivated( disposables =>
        {
            this.WhenAnyValue( x => x.ViewModel )
                .WhereNotNull()
                .Do( vm => PopulateFromViewModel( this , vm , disposables ) )
                .Subscribe()
                .DisposeWith( disposables );
        } );
    }

    private static void PopulateFromViewModel( MainView view , IngotsViewModel viewModel , CompositeDisposable disposables )
    {
        viewModel.PickExistingDatabaseInteraction.RegisterHandler( async ctx =>
            {
                var topLevel = TopLevel.GetTopLevel(view)!;
                var files = await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
                {
                    Title = "Open Text File",
                    AllowMultiple = false
                });

                if ( files.Count >= 1 )
                    ctx.SetOutput( files[0].TryGetLocalPath() ?? string.Empty );
                else 
                    ctx.SetOutput( string.Empty );
            } )
            .DisposeWith( disposables );

        view.BindCommand( viewModel ,
                vm => vm.PickExistingDatabase ,
                v => v.ButtonPickFile )
            .DisposeWith( disposables );

        viewModel.PickNewDatabaseInteraction.RegisterHandler( async ctx =>
            {
                var topLevel = TopLevel.GetTopLevel(view)!;
                var file = await topLevel.StorageProvider.SaveFilePickerAsync(new FilePickerSaveOptions
                {
                    Title = "Open Text File"
                });

                if ( file != null )
                    ctx.SetOutput( file.TryGetLocalPath() ?? string.Empty );
                else 
                    ctx.SetOutput( string.Empty );
            } )
            .DisposeWith( disposables );
        
        view.BindCommand( viewModel ,
                vm => vm.PickNewDatabase ,
                v => v.ButtonCreateFile )
            .DisposeWith( disposables );
    }
}
