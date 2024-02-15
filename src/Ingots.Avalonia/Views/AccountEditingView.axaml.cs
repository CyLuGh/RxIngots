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
                vm => vm.Iban ,
                v => v.TextBoxIban.Text )
            .DisposeWith( disposables );
        
        view.Bind( viewModel ,
                vm => vm.Bic ,
                v => v.TextBoxBic.Text )
            .DisposeWith( disposables );
        
        view.Bind( viewModel ,
                vm => vm.Description ,
                v => v.TextBoxDescription.Text )
            .DisposeWith( disposables );

        view.Bind( viewModel ,
                vm => vm.Kind ,
                v => v.ComboBoxKind.SelectedItem )
            .DisposeWith( disposables );
        
        view.Bind( viewModel ,
                vm => vm.Bank ,
                v => v.TextBoxBank.Text )
            .DisposeWith( disposables );
        
        view.Bind( viewModel ,
                vm => vm.Stash ,
                v => v.TextBoxStash.Text )
            .DisposeWith( disposables );

        view.Bind( viewModel ,
                vm => vm.StartValue ,
                v => v.NumericUpDownStartValue.Value )
            .DisposeWith( disposables );

        view.OneWayBind( viewModel ,
                vm => vm.HasValidIban ,
                v => v.TextBlockWarning.IsVisible,
                b => !b)
            .DisposeWith( disposables );
    }
}