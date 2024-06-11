using GraafschapCollegeApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GraafschapCollegeApi.Controllers
{
    [Route("api/vehicles")]
    [Authorize]
    [ApiController]
    public class VehicleController(VehicleService service) : ControllerBase
    {
        [HttpGet]
        public IActionResult GetVehicles()
        {
            var vehicles = service.GetVehicles();

            return Ok(vehicles);
        }
    }
}
