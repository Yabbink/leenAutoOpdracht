namespace GraafschapCollege.BlazorApp;

using GraafschapCollege.BlazorApp.Handlers;
using GraafschapCollege.BlazorApp.Services;
using GraafschapCollege.BlazorApp.State;
using GraafschapCollege.Shared.Clients;
using GraafschapCollege.Shared.Options;

using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");

        // Add options pattern to the configuration
        builder.Services.Configure<ApiOptions>(builder.Configuration.GetSection(ApiOptions.SectionName));

        // Add Services
        builder.Services.AddScoped<LocalStorageService>();

        // Add API Clients and Handlers
        builder.Services.AddScoped<AuthorizationMessageHandler>();

        builder.Services.AddScoped<UserHttpClient>();
        builder.Services.AddHttpClient(nameof(UserHttpClient)).AddHttpMessageHandler<AuthorizationMessageHandler>();

        builder.Services.AddScoped<ReservationHttpClient>();
        builder.Services.AddHttpClient(nameof(ReservationHttpClient)).AddHttpMessageHandler<AuthorizationMessageHandler>();

        builder.Services.AddScoped<VehicleHttpClient>();
        builder.Services.AddHttpClient(nameof(VehicleHttpClient)).AddHttpMessageHandler<AuthorizationMessageHandler>();

        builder.Services.AddScoped<AuthHttpClient>();

        // Add Auth
        builder.Services.AddScoped<GraafschapCollegeAuthenticationStateProvider>();
        builder.Services.AddScoped<AuthenticationStateProvider>(sp => sp.GetRequiredService<GraafschapCollegeAuthenticationStateProvider>());
        builder.Services.AddAuthorizationCore();


        await builder.Build().RunAsync();
    }
}