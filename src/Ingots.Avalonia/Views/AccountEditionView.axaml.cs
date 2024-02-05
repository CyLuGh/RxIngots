using Avalonia.ReactiveUI;
using Ingots.ViewModels;
using ReactiveUI;
using SukiUI.Controls;
using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace Ingots.Avalonia.Views;

public partial class AccountEditionView : ReactiveUserControl<AccountEditionViewModel>
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

    private static void PopulateFromViewModel( AccountEditionView view , AccountEditionViewModel viewModel ,
        CompositeDisposable disposables )
    {
        view.OneWayBind( viewModel ,
                vm => vm.Account ,
                v => v.EditingView.ViewModel )
            .DisposeWith( disposables );

        view.BindCommand( viewModel ,
                vm => vm.AddAccount ,
                v => v.ButtonSave,
                viewModel.WhenAnyValue( x=>x.Account ))
            .DisposeWith( disposables );

        view.BindCommand( viewModel ,
                vm => vm.CloseDialog ,
                v => v.ButtonCancel )
            .DisposeWith( disposables );

        viewModel.CloseDialogInteraction.RegisterHandler( _ => SukiHost.CloseDialog() )
            .DisposeWith( disposables );
    }
}