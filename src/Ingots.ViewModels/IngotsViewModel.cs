using LanguageExt;
using ReactiveUI.Fody.Helpers;

namespace Ingots.ViewModels;

public class IngotsViewModel : ViewModelBase
{
    public Seq<AccountViewModel> Accounts { [ObservableAsProperty] get; }
}