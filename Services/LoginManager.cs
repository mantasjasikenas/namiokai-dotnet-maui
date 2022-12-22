
using Namiokai.Auth0;

namespace Namiokai.Services;

public class LoginManager
{

    private readonly Auth0Client auth0Client;

    public static AuthUser AuthUser { get; set; }

    public LoginManager(Auth0Client auth0Client)
    {
        AuthUser ??= new AuthUser();
        this.auth0Client = auth0Client;

        //#if WINDOWS
        //        auth0Client.Browser = new WebViewBrowserAuthenticator(WebViewInstance);
        //#endif
    }


    public async Task<bool> LoginAsync()
    {
        var loginResult = await auth0Client.LoginAsync();

        if (!loginResult.IsError)
        {
            AuthUser = new AuthUser
            {
                UserName = loginResult.User.Identity.Name,
                ImgSource = loginResult.User.Claims.FirstOrDefault(c => c.Type == "picture")?.Value,
                //Email = loginResult.User.Claims.FirstOrDefault(c => c.Type == "email")?.Value,
                Email = "-",
                Admin = loginResult.User.Claims.FirstOrDefault(c => c.Type == "sub").Value.Equals("google-oauth2|114486168866293310146")
            };

            await SecureStorage.SetAsync(nameof(AuthUser), AuthUser.ToString());

            return true;
        }

        return false;

    }

    public async Task<bool> LogoutAsync()
    {
        var logoutResult = await auth0Client.LogoutAsync();
        AuthUser?.Clear();
        SecureStorage.Remove(nameof(AuthUser));

        return !logoutResult.IsError;
    }
}
