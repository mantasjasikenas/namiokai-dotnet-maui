﻿// Auth0/WebBrowserAuthenticator.cs

using IdentityModel.Client;
using IdentityModel.OidcClient.Browser;

namespace Namiokai.Auth0;

public class WebBrowserAuthenticator : IdentityModel.OidcClient.Browser.IBrowser
{
    public async Task<BrowserResult> InvokeAsync(BrowserOptions options, CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await WebAuthenticator.Default.AuthenticateAsync(
                new Uri(options.StartUrl),
                new Uri(options.EndUrl));

            var url = new RequestUrl(options.EndUrl)
                .Create(new Parameters(result.Properties));

            return new BrowserResult
            {
                Response = url,
                ResultType = BrowserResultType.Success
            };
        }
        catch (TaskCanceledException)
        {
            return new BrowserResult
            {
                ResultType = BrowserResultType.UserCancel,
                ErrorDescription = Resources.Login_canceled_by_the_user
            };
        }
    }
}