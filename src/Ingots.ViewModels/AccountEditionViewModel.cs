using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Ingots.ViewModels;

public class AccountEditionViewModel : ViewModelBase
{
    [Reactive] public AccountViewModel Account { get; set; }
}