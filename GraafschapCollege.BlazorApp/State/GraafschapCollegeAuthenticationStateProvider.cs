namespace GraafschapCollege.BlazorApp.State;

using GraafschapCollege.BlazorApp.Services;
using GraafschapCollege.Shared.Constants;
using GraafschapCollege.Shared.Interfaces;

using Microsoft.AspNetCore.Components.Authorization;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

using static GraafschapCollege.Shared.Interfaces.ICurrentUserContext;

public class GraafschapCollegeAuthenticationStateProvider : AuthenticationStateProvider, ICurrentUserContext
{
    private readonly LocalStorageService localStorage;

    public CurrentUser User { get; private set; }

    public bool IsAuthenticated { get; private set; }

    public GraafschapCollegeAuthenticationStateProvider(LocalStorageService localStorage)
    {
        this.localStorage = localStorage;
        AuthenticationStateChanged += OnAuthenticationStateChangedAsync;
    }

    public bool IsInRole(string roleName)
    {
        return User.Roles.Contains(roleName);
    }

    public async Task Logout()
    {
        await localStorage.RemoveItemAsync("token");
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(new ClaimsPrincipal())));
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var token = await localStorage.GetItemAsync("token");
        var principal = new ClaimsPrincipal();

        if (!string.IsNullOrEmpty(token))
        {
            principal = CreateClaimsPrincipalFromToken(token);
        }

        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(principal)));

        return new(principal);
    }

    private async void OnAuthenticationStateChangedAsync(Task<AuthenticationState> task)
    {
        var authenticationState = await task;

        IsAuthenticated = authenticationState.User.Identity?.IsAuthenticated ?? false;

        if (IsAuthenticated == true)
        {
            User = CurrentUser.FromClaimsPrincipal(authenticationState.User);
        }
    }

    private static ClaimsPrincipal CreateClaimsPrincipalFromToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var identity = new ClaimsIdentity();

        if (tokenHandler.CanReadToken(token))
        {
            var jwtSecurityToken = tokenHandler.ReadJwtToken(token);

            identity = new(
                jwtSecurityToken.Claims,
                authenticationType: "Bearer Token",
                nameType: Claims.Name,
                roleType: Claims.Role);
        }

        return new(identity);
    }
}