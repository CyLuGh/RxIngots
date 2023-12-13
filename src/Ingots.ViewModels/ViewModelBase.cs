using ReactiveUI;

namespace Ingots.ViewModels;

public class ViewModelBase : ReactiveObject, IActivatableViewModel
{
    public ViewModelActivator Activator { get; } = new();
}