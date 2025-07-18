using Disaster_demo.Models.Entities;
using Disaster_demo.Services;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;

namespace Disaster_demo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SymptomsController : ControllerBase
    {
        private readonly ISymptomsServices _symptomServices;

        public SymptomsController(ISymptomsServices SymptomsService)
        {
            this._symptomServices = SymptomsService;
        }

        //[HttpGet("all")]
        //public async Task<IActionResult> getSymptoms()
        //{


        //    var symptoms = await this._symptomServices.getPendingSymptoms();
        //    return Ok(symptoms);
        //}



        //[HttpPost("approve-symptom/{id}")]
        //public async Task<IActionResult> ApproveSymptom(int id)
        //{
        //    var result = await _symptomServices.ApproveSymptom(id);
        //    if (!result)
        //        return NotFound();

        //    return Ok();
        //}

        //[HttpPost("create")]
        //public async Task<IActionResult> CreateSymptoms([FromBody] Symptoms symptoms)
        //{

        //    if (symptoms == null)
        //    {
        //        return BadRequest("Symptom data is null.");
        //    }

        //    try
        //    {

        //        await _symptomServices.createSymptoms(symptoms);
        //        return Ok("Symptom created successfully.");
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the exception (you can use a logging service here)
        //        return StatusCode(500, $"Internal server error: {ex.Message}");
        //    }
        //}

        [HttpPost("create")]
        public async Task<IActionResult> CreateSymptoms([FromBody] Symptoms symptoms)
        {
            if (symptoms == null)
            {
                return BadRequest("Symptom data is null.");
            }

            // Log incoming data for debugging
            Console.WriteLine($"Received report: {symptoms.reporter_name}, {symptoms.contact_no}, {symptoms.district}, {symptoms.divisional_secretariat}, {symptoms.date_time}, {symptoms.description}");

            try
            {
                await _symptomServices.createSymptoms(symptoms);
                return Ok("Symptom created successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        //[HttpPost("create")]
        //[Consumes("multipart/form-data")]
        //public async Task<IActionResult> CreateSymptoms([FromForm] Symptoms symptoms)
        //{
        //    if (symptoms == null)
        //    {
        //        return BadRequest("Symptom data is null.");
        //    }

        //    try
        //    {
        //        await _symptomServices.createSymptoms(symptoms);
        //        return Ok("Symptom created successfully.");
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Internal server error: {ex.Message}");
        //    }
        //}


        //[HttpGet("pendingReport")]
        //public IActionResult GetPendingSymptoms()
        //{
        //    var reports = _symptomServices.GetPendingSymptoms();
        //    return Ok(reports);
        //}

        //[HttpGet("pendingReports")]
        //public IActionResult GetPendingSymptoms([FromQuery] string gnDivision)
        //{
        //    if (string.IsNullOrEmpty(gnDivision))
        //        return BadRequest("GN Division is required.");

        //    var reports = _symptomServices.GetPendingSymptomsByGnDivision(gnDivision);
        //    return Ok(reports);
        //}

        [HttpGet("pendingReports")]
        public async Task<IActionResult> GetPendingReportsByDivision([FromQuery] string divisionalSecretariat)
        {
            if (string.IsNullOrWhiteSpace(divisionalSecretariat))
            {
                return BadRequest("Divisional Secretariat is required.");
            }
            var reports = await this._symptomServices.GetPendingReportsByDivisionAsync(divisionalSecretariat);

            
            return Ok(reports);
        }



        [HttpPost("updateStatus")]
        public IActionResult UpdateSymptomStatus([FromBody] StatusUpdateModel model)
        {
            if (model == null || string.IsNullOrEmpty(model.Status))
                return BadRequest("Invalid request data");

            var updated = _symptomServices.UpdateSymptomStatus(model.ReportId, model.Status);

            if (updated)
                return Ok(new { success = true, message = "Status updated successfully" });

            return NotFound(new { success = false, message = "Report not found" });
        }

        [HttpGet("approvedByDs/{district}")]
        public async Task<IActionResult> GetDsApprovedSymptomsByDistrict(string district)
        {
            var reports = await _symptomServices.GetDsApprovedSymptomsByDistrictAsync(district);
            return Ok(reports);
        }



    }
}
