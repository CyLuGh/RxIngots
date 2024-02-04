using Ingots.Data;
using Ingots.Data.Sqlite;
using Ingots.ViewModels;
using Splat;

namespace Ingots.Avalonia;

public sealed class ViewModelLocator
{
    public static ViewModelLocator Instance { get; } = new();
    
    private ViewModelLocator()
    {
        SplatRegistrations.RegisterLazySingleton<IngotsViewModel>();
        SplatRegistrations.RegisterLazySingleton<IDataManager,DataManager>();
        SplatRegistrations.Register<AccountEditionViewModel>();
        
        SplatRegistrations.SetupIOC();
    }

    public IngotsViewModel IngotsViewModel => Locator.Current.GetService<IngotsViewModel>()!;
}