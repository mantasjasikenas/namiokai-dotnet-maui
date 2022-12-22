using System.Text.Json;
using Namiokai.Auth0;
using Debug = System.Diagnostics.Debug;

namespace Namiokai.Services;

public static class ServicesExtensions
{
    public static MauiAppBuilder ConfigureServices(this MauiAppBuilder builder)
    {
        var sheetsCredentials = Utils.ReadCredentials("sheets_credentials.json");
        var auth0Credentials = Utils.ReadCredentials("auth0_credentials.json");
        var auth0ClientOptions = JsonSerializer.Deserialize<Auth0ClientOptions>(auth0Credentials);

#if WINDOWS
        auth0ClientOptions.RedirectUri = "http://localhost/callback";
#else
        auth0ClientOptions.RedirectUri = "myapp://callback";
#endif
        
        builder.Services.AddSingleton(Connectivity.Current);
        builder.Services.AddSingleton(serviceProvider => ActivatorUtilities.CreateInstance<SheetsManager>(serviceProvider, sheetsCredentials));
        builder.Services.AddSingleton(new Auth0Client(auth0ClientOptions));


        return builder;
    }
}