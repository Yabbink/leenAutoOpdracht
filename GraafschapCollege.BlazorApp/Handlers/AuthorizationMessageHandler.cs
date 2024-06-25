namespace GraafschapCollege.BlazorApp.Handlers;
using GraafschapCollege.BlazorApp.Services;
using System.Net.Http.Headers;

public class AuthorizationMessageHandler(LocalStorageService localStorageService) : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var token = await localStorageService.GetItemAsync("token");

        if (!string.IsNullOrEmpty(token))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
        else
        {
            throw new ArgumentNullException(nameof(request), "Token is not found and cant be added");
        }

        return await base.SendAsync(request, cancellationToken);
    }
}