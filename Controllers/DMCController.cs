using Disaster_demo.Services;
using Microsoft.AspNetCore.Mvc;

namespace Disaster_demo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DMCController : ControllerBase
    {
        private readonly IDMCOfficerServices _dmcServices;

        public DMCController(IDMCOfficerServices DMCService)
        {
            this._dmcServices = DMCService;
        }

        [HttpGet("login/{userId}")]
        public async Task<IActionResult> GetDMCOfficerDetails(string userId)
        {
            var result = await _dmcServices.GetDMCOfficerDetailsAsync(userId);
            if (result == null) return NotFound(new { message = "DMC Officer not found" });
            return Ok(result);
        }
    }

}
