using CommunityToolkit.Maui;
using Plugin.LocalNotification;

namespace Namiokai;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
#if ANDROID || IOS
            .UseLocalNotification()
#endif
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            })
            .ConfigureServices()
            .ConfigurePagesAndViewModels();
        
        
        return builder.Build();
    }
}