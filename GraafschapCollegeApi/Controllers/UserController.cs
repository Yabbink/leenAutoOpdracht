using GraafschapCollege.Shared;
using GraafschapCollegeApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace GraafschapCollegeApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        public static List<User> Users = new List<User>
        {
            new User
            {
                name = "John Doe",
                email = "johndoe@gmail.com",
                password = "password123"
            },
        };

        [HttpGet]
        public IActionResult Get()
        {
            var userDtos = Users.Select(user => new UserDto
            {
                name = user.name,
                email = user.email
            });

            return Ok(userDtos);
        }

        [HttpPost]
        public IActionResult Post([FromBody] User user)
        {
            Users.Add(user);
            return Ok();
        }
    }
}
