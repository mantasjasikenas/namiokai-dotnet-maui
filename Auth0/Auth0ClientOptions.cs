using Newtonsoft.Json;

namespace Namiokai.Auth0;

public class Auth0ClientOptions
{
    public Auth0ClientOptions()
    {
        Scope = "openid";
        RedirectUri = "myapp://callback";
        Browser = new WebBrowserAuthenticator();
    }

    [JsonRequired]
    public string Domain { get; set; }

    [JsonRequired]
    public string ClientId { get; set; }

    [JsonRequired]
    public string Scope { get; set; }

    public string RedirectUri { get; set; }

    public IdentityModel.OidcClient.Browser.IBrowser Browser { get; set; }
}