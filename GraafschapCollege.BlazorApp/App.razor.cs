namespace GraafschapCollege.BlazorApp;

using GraafschapCollege.BlazorApp.State;

using Microsoft.AspNetCore.Components;

public partial class App
{
    [Inject]
    public GraafschapCollegeAuthenticationStateProvider Context { get; set; }
}