using GraafschapCollegeApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using GraafschapCollege.Shared.Requests;
using Microsoft.AspNetCore.Mvc;

namespace GraafschapCollegeApi.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/auth")]
    public class AuthController(AuthService authService) : ControllerBase
    {
        [HttpPost]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var response = authService.Login(request);

            if (response == null)
            {
                return Unauthorized();
            }

            return Ok(response);
        }
    }
}
