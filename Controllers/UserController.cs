using Disaster_demo.Models.Entities;
using Disaster_demo.Services;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace Disaster_demo.Controllers
{
    [Controller]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _userServices;

        public UserController(IUserServices userServices)
        {
            _userServices = userServices;
        }

        //[HttpPost("login")]
        //public async Task<IActionResult> Login([FromBody] LoginDTO dto)
        //{
        //    var result = await _userServices.LoginAsync(dto);

        //    if (result == null || result.Role == default(UserRole))
        //    {
        //        return Unauthorized(result?.Message ?? "Invalid credentials.");
        //    }


        //    return Ok(result);
        //}

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO dto)
        {
            var result = await _userServices.LoginAsync(dto);

            if (result == null || string.IsNullOrEmpty(result.Role))
            {
                return Unauthorized(result?.Message ?? "Invalid credentials.");
            }

            return Ok(result);
        }






    }

}
