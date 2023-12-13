using Ingots.ViewModels;
using Splat;

namespace Ingots.Avalonia;

public sealed class ViewModelLocator
{
    public static ViewModelLocator Instance { get; } = new();
    
    private ViewModelLocator()
    {
        SplatRegistrations.RegisterLazySingleton<IngotsViewModel>();
    }

    public IngotsViewModel IngotsViewModel => Locator.Current.GetService<IngotsViewModel>()!;
}