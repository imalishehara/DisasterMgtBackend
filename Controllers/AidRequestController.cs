using Disaster_demo.Models.Entities;
using Disaster_demo.Services;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;

namespace Disaster_demo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AidRequestController : ControllerBase
    {
        private readonly IAidRequestServices _aidrequestServices;

        public AidRequestController(IAidRequestServices aidRequestServices)
        {
            this._aidrequestServices = (AidRequestServices)aidRequestServices;
        }

        //[HttpGet("all")]
        //public async Task<IActionResult> getAidRequests()
        //{


        //    var approved = await this._aidrequestServices.getPendingAidRequests();
        //    return Ok(approved);
        //}

        //[HttpPost("create")]
        //public async Task<IActionResult> CreateSymptoms([FromBody] Symptoms symptoms)
        //{
        //    Console.WriteLine("CreateSymptoms endpoint hit");
        //    {

        //        if (!ModelState.IsValid)
        //            return BadRequest(ModelState);

        //        var result = await _symptomServices.SubmitSymptomsAsync(symptoms);
        //        return Ok(new { success = true, data = result });
        //    }
        //}



        //[HttpPost("create")]
        //public async Task<IActionResult> CreateAidRequest([FromBody] AidRequests aidrequests)
        //{

        //    if (aidrequests == null)
        //    {
        //        return BadRequest("Symptom data is null.");
        //    }

        //    try
        //    {

        //        await _aidrequestServices.createAidRequest(aidrequests);
        //        return Ok("Request created successfully.");
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the exception (you can use a logging service here)
        //        return StatusCode(500, $"Internal server error: {ex.Message}");
        //    }
        //}

        [HttpPost("create")]
        public async Task<IActionResult> CreateAidRequest([FromBody] AidRequests request)
        {
            bool result = await _aidrequestServices.CreateAidRequestAsync(request);
            if (!result)
            {
                return BadRequest("Invalid request type.");
            }

            return Ok(new { message = "Aid request submitted!" });
        }

        //[HttpGet("pending")]
        //public async Task<IActionResult> GetPendingAidRequests()
        //{
        //    var result = await _aidrequestServices.getPendingAidRequest(); 
        //    return Ok(result); // This now returns List<AidRequests>
        //}

        [HttpGet("pending")]
        public async Task<IActionResult> GetPendingAidRequests([FromQuery] string divisionalSecretariat)
        {
            if (string.IsNullOrEmpty(divisionalSecretariat))
            {
                return BadRequest("Divisional Secretariat is required.");
            }

            var result = await _aidrequestServices.getPendingAidRequests(divisionalSecretariat);
            return Ok(result);
        }



        //[HttpPost("updateStatus")]
        //public IActionResult UpdateSymptomStatus([FromBody] StatusUpdateModel model)
        //{
        //    if (model == null || string.IsNullOrEmpty(model.Status))
        //        return BadRequest("Invalid request data");

        //    var updated = _aidrequestServices.UpdateSymptomStatus(model.ReportId, model.Status);

        //    if (updated)
        //        return Ok(new { success = true, message = "Status updated successfully" });

        //    return NotFound(new { success = false, message = "Report not found" });
        //}

        [HttpPost("updateStatus")]
        public IActionResult UpdateGnStatus([FromBody] StatusUpdateModel model)
        {
            var success = _aidrequestServices.UpdateStatus(model);
            if (!success)
                return NotFound(new { message = "Aid request not found." });

            return Ok(new { message = "Status updated successfully." });
        }

        [HttpGet("byDistrict/{district}")]
        public async Task<IActionResult> GetAidRequestsByDistrict(string district)
        {
            if (string.IsNullOrEmpty(district))
                return BadRequest("District is required.");

            var requests = await _aidrequestServices.GetAidRequestsByDistrict(district);
            return Ok(requests);
        }

        [HttpGet("dmc-approved")]
        public async Task<ActionResult<List<AidRequests>>> GetDmcApprovedAidRequests()
        {
            var result = await _aidrequestServices.GetDmcApprovedAidRequests();
            return Ok(result);
        }

        [HttpGet("all-dmc")]
        public async Task<ActionResult<List<AidRequests>>> GetAllDmcRelatedAidRequests([FromQuery] string district)
        {
            if (string.IsNullOrEmpty(district))
                return BadRequest("District is required.");

            var result = await _aidrequestServices.GetAllDmcRelatedAidRequests(district);
            return Ok(result);
        }






    }
}
