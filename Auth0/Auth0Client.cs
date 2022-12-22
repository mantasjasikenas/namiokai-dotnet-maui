using IdentityModel.Client;
using IdentityModel.OidcClient;
using IdentityModel.OidcClient.Browser;

namespace Namiokai.Auth0;

public class Auth0Client
{
    private readonly OidcClient _oidcClient;

    public Auth0Client(Auth0ClientOptions options)
    {
        _oidcClient = new OidcClient(new OidcClientOptions
        {
            Authority = $"https://{options.Domain}",
            ClientId = options.ClientId,
            Scope = options.Scope,
            RedirectUri = options.RedirectUri,
            Browser = options.Browser
        });
    }

    public IdentityModel.OidcClient.Browser.IBrowser Browser
    {
        get => _oidcClient.Options.Browser;
        set => _oidcClient.Options.Browser = value;
    }

    public async Task<LoginResult> LoginAsync() => await _oidcClient.LoginAsync();

    public async Task<BrowserResult> LogoutAsync()
    {
        var logoutParameters = new Dictionary<string, string>
    {
      {"client_id", _oidcClient.Options.ClientId },
      {"returnTo", _oidcClient.Options.RedirectUri }
    };

        var logoutRequest = new LogoutRequest();
        var endSessionUrl = new RequestUrl($"{_oidcClient.Options.Authority}/v2/logout")
          .Create(new Parameters(logoutParameters));
        var browserOptions = new BrowserOptions(endSessionUrl, _oidcClient.Options.RedirectUri)
        {
            Timeout = TimeSpan.FromSeconds(logoutRequest.BrowserTimeout),
            DisplayMode = logoutRequest.BrowserDisplayMode
        };

        var browserResult = await _oidcClient.Options.Browser.InvokeAsync(browserOptions);

        return browserResult;
    }
}