namespace GraafschapCollege.BlazorApp.Layout;

using GraafschapCollege.BlazorApp.State;

using Microsoft.AspNetCore.Components;

public partial class MainLayout
{
    [Inject]
    private NavigationManager NavigationManager { get; set; }

    [Inject]
    private GraafschapCollegeAuthenticationStateProvider AuthState { get; set; }

    private async Task Logout()
    {
        await AuthState.Logout();
        NavigationManager.NavigateTo("/login");
    }
}