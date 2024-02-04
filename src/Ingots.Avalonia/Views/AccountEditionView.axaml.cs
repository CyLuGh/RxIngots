using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Ingots.ViewModels;
using ReactiveUI;
using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace Ingots.Avalonia.Views;

public partial class AccountEditionView : ReactiveUserControl<AccountViewModel>
{
    public AccountEditionView()
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
    
    private static void PopulateFromViewModel(AccountEditionView view,AccountViewModel viewModel,CompositeDisposable disposables){}
}