using GraafschapCollege.Shared.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GraafschapCollege.Shared.Interfaces
{
    public interface ICurrentUserContext
    {
        public CurrentUser User { get; }
        public bool IsAuthenticated { get; }

        public bool IsInRole(string roleName);

        public class CurrentUser
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public List<string> Roles { get; set; }

            public static CurrentUser FromClaimsPrincipal(ClaimsPrincipal principal)
            {
                var id = principal.FindFirst(Claims.Id)!.Value;
                var name = principal.FindFirst(Claims.Name)!.Value;
                var email = principal.FindFirst(Claims.Email)!.Value;
                var roles = principal.FindAll(Claims.Role).Select(r => r.Value).ToList();

                return new CurrentUser
                {
                    Id = int.Parse(id),
                    Name = name,
                    Email = email,
                    Roles = roles
                };
            }
        }
    }
}
