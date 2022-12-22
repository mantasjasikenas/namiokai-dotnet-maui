using CommunityToolkit.Maui;

namespace Namiokai.ViewModels;

public static class ViewModelsExtensions
{
    public static MauiAppBuilder ConfigurePagesAndViewModels(this MauiAppBuilder builder)
    {
        // Views & viewmodels
        builder.Services.AddSingleton<MainPage, MainViewModel>();
        builder.Services.AddSingleton<LoginManager>();
        builder.Services.AddSingletonWithShellRoute<SettingsPage, SettingsViewModel>(nameof(SettingsPage));

        builder.Services.AddTransient<NoInternet>();
        builder.Services.AddTransientWithShellRoute<AddPage, AddViewModel>(nameof(AddPage));
        builder.Services.AddTransientWithShellRoute<FuelPage, FuelViewModel>(nameof(FuelPage));

        return builder;
    }
}