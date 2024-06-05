using GraafschapCollege.Shared.Interfaces;
using Microsoft.AspNetCore.Http;
using static GraafschapCollege.Shared.Interfaces.ICurrentUserContext;

namespace GraafschapCollegeApi.Context
{
    public class CurrentUserContext(IHttpContextAccessor httpContextAccessor) : ICurrentUserContext
    {
        public CurrentUser User => CurrentUser.FromClaimsPrincipal(httpContextAccessor.HttpContext!.User);

        public bool IsAuthenticated => httpContextAccessor.HttpContext!.User.Identity?.IsAuthenticated ?? false;

        public bool IsInRole(string roleName)
        {
            return User.Roles.Contains(roleName);
        }
    }
}
