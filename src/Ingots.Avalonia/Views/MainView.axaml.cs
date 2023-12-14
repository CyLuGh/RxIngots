using Avalonia.Controls;
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
    }
}
