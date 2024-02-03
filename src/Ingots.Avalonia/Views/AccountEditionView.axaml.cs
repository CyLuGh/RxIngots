using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Ingots.ViewModels;

namespace Ingots.Avalonia.Views;

public partial class AccountEditionView : ReactiveUserControl<AccountViewModel>
{
    public AccountEditionView()
    {
        InitializeComponent();
    }
}