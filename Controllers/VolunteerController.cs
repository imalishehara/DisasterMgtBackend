using Disaster_demo.Models.Entities;
using Disaster_demo.Services;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;


namespace Disaster_demo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VolunteerController : ControllerBase
    {
        private readonly IVolunteerServices _volunteerServices;

        public VolunteerController(IVolunteerServices VolunteerService)
        {
            this._volunteerServices = VolunteerService;
        }


        //[HttpPost("signup")]
        //public async Task<IActionResult> Signup([FromBody] VolunteerSignupDTO dto)
        //{
        //    var result = await _volunteerServices.SignupAsync(dto);

        //    if (result == "Email already registered.")
        //        return BadRequest(result);

        //    return Ok(result);
        //}

        [HttpPost("signup")]
        public async Task<IActionResult> Signup([FromBody] VolunteerSignupDTO dto)
        {
            var userId = await _volunteerServices.SignupAsync(dto);

            if (userId == -1)
                return BadRequest("Email already registered.");

            return Ok(new { userId });  // Return JSON object with userId
        }

        //[HttpGet("all")]
        //public async Task<IActionResult> GetAllVolunteers()
        //{
        //    var volunteers = await _volunteerServices.GetAllVolunteersAsync();

        //    if (volunteers == null || !volunteers.Any())
        //        return NotFound("No volunteers found.");

        //    return Ok(volunteers);
        //}

        [HttpGet("by-division")]
        public async Task<IActionResult> GetVolunteersByDivision([FromQuery] string divisionalSecretariat)
        {
            if (string.IsNullOrEmpty(divisionalSecretariat))
                return BadRequest("Divisional Secretariat is required.");

            var volunteers = await _volunteerServices.GetVolunteersByDsDivisionAsync(divisionalSecretariat);

            if (volunteers == null || !volunteers.Any())
                return NotFound("No volunteers found for the given division.");

            return Ok(volunteers);
        }


        [HttpGet("details/{userId}")]
        public async Task<IActionResult> GetVolunteerDetails(string userId)
        {
            var details = await _volunteerServices.GetVolunteerDetailsAsync(userId);
            if (details == null)
                return NotFound();

            return Ok(details);
        }


        [HttpGet("by-district")]
        public async Task<IActionResult> GetVolunteersByDistrict([FromQuery] string district)
        {
            if (string.IsNullOrEmpty(district))
                return BadRequest("District is required.");

            var volunteers = await _volunteerServices.GetVolunteersByDistrictAsync(district);

            if (volunteers == null || !volunteers.Any())
                return NotFound("No volunteers found for the given district.");

            return Ok(volunteers);
        }

        [HttpPut("update-availability")]
        public async Task<IActionResult> UpdateAvailability([FromQuery] int userId, [FromQuery] string newStatus)
        {
            if (!Enum.TryParse<AvailabilityStatus>(newStatus, true, out var status))
                return BadRequest("Invalid availability status.");

            var updated = await _volunteerServices.UpdateAvailabilityAsync(userId, status);
            if (!updated)
                return NotFound("Volunteer not found.");

            return Ok("Availability updated successfully.");
        }




    }
}

 
