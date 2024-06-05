using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using GraafschapCollegeApi.Entities;
using System.Text;
using GraafschapCollege.Shared.Constants;
using Microsoft.Extensions.Options;
using System.Linq;
using GraafschapCollege.Shared.Options;

namespace GraafschapCollegeApi
{
    public class TokenService(IOptions<JwtOptions> options)
    {
        private readonly SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(options.Value.Key));

        public string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new(Claims.Id, user.Id.ToString()),
                new(Claims.Name, user.Name),
                new(Claims.Email, user.Email),
            };

            claims.AddRange(user.Roles.Select(role => new Claim(Claims.Role, role.Name)));

            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddDays(7),
                signingCredentials: signingCredentials,
                issuer: options.Value.Issuer,
                audience: options.Value.Audience);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
