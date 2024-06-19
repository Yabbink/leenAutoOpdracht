namespace GraafschapCollege.BlazorApp.Pages.Login;
using GraafschapCollege.BlazorApp.Services;
using GraafschapCollege.BlazorApp.State;
using GraafschapCollege.Shared.Clients;
using GraafschapCollege.Shared.Requests;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

[AllowAnonymous]
[Route("/login")]
public partial class Login
{
    private readonly LoginRequest Request = new();

    [Inject]
    private AuthHttpClient AuthHttpClient { get; set; }

    [Inject]
    private LocalStorageService LocalStorageService { get; set; }

    [Inject]
    private GraafschapCollegeAuthenticationStateProvider AuthState { get; set; }

    [Inject]
    private NavigationManager NavigationManager { get; set; }

    private async Task LoginAsync()
    {
        var response = await AuthHttpClient.LoginAsync(Request);

        if (response is null)
        {
            return;
        }

        await LocalStorageService.SetItemAsync("token", response.Token);
        await AuthState.GetAuthenticationStateAsync();

        NavigationManager.NavigateTo("/");
    }
}