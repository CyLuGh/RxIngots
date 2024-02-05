using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Ingots.Core;
using Ingots.ViewModels;
using ReactiveUI;
using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace Ingots.Avalonia.Views;

public partial class AccountEditingView : ReactiveUserControl<AccountViewModel>
{
    public AccountEditingView()
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

    private static void PopulateFromViewModel( AccountEditingView view , AccountViewModel viewModel ,
        CompositeDisposable disposables )
    {
        view.ComboBoxKind.ItemsSource = Enum.GetValues<AccountKind>();
        
        view.Bind( viewModel ,
                vm => vm.Description ,
                v => v.TextBoxDescription.Text )
            .DisposeWith( disposables );

        view.Bind( viewModel ,
                vm => vm.Kind ,
                v => v.ComboBoxKind.SelectedItem )
            .DisposeWith( disposables );
    }

}