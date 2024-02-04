using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Ingots.ViewModels;
using ReactiveUI;
using Splat;
using SukiUI.Controls;
using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace Ingots.Avalonia.Views;

public partial class AccountsView : ReactiveUserControl<IngotsViewModel>
{
    public AccountsView()
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

    private static void PopulateFromViewModel( AccountsView view , IngotsViewModel viewModel ,
        CompositeDisposable disposables )
    {
        viewModel.EditAccountInteraction.RegisterHandler( ctx =>
        {
            var vm = Locator.Current.GetService<AccountEditionViewModel>()!;
            vm.Account = ctx.Input;
            SukiHost.ShowDialog( new AccountEditionView(){ViewModel = vm}, allowBackgroundClose: true );
        } ).DisposeWith( disposables );

        view.BindCommand( viewModel ,
                vm => vm.EditAccount ,
                v => v.ButtonAddAccount ,
                Observable.Return( new AccountViewModel() ))
            .DisposeWith( disposables );
    }
}