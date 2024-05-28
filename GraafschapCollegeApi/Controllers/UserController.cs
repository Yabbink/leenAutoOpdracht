using GraafschapCollege.Shared;
using GraafschapCollege.Shared.Constants;
using GraafschapCollege.Shared.Requests;
using GraafschapCollegeApi.Models;
using GraafschapCollegeApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GraafschapCollegeApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserController(UserService userService) : ControllerBase
    {
        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = userService.GetUsers();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            var response = userService.GetUserById(id);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpPost]
        public IActionResult CreateUser(CreateUserRequest request)
        {
            var response = userService.CreateUser(request);

            return CreatedAtAction(nameof(CreateUser), new { id = response.Id }, response);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, UpdateUserRequest user)
        {
            var updatedUser = userService.UpdateUser(id, user);

            if (updatedUser == null)
            {
                return NotFound();
            }

            return Ok(updatedUser);
        }

        [Authorize(Roles = Roles.Administrator)]
        [HttpGet("secret")]
        public IActionResult Secret()
        {
            return Ok("This is a secret message");
        }
    }
}
