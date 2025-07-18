using Disaster_demo.Models.Entities;
using Disaster_demo.Services;
using Microsoft.AspNetCore.Mvc;

namespace Disaster_demo.Controllers
{
    [ApiController] // Add this attribute to enable controller features
    [Route("[controller]")] // Define a base route for the controller
    public class AlertsController : ControllerBase // Inherit from ControllerBase
    {
        private readonly AlertServices _alertServices;

        public AlertsController(AlertServices alertServices)
        {
            _alertServices = alertServices;
        }





        //[HttpGet("all")]
        //public async Task<IActionResult> getAlerts() 
        //{
        //    var alerts = await _alertServices.GetAllAlertsAsync();

        //    var formatted = alerts.Select(alert => new
        //    {
        //        id = alert.alert_id,
        //        type = alert.alert_type,
        //        district = alert.district,
        //        gnDivision = alert.gn_division,
        //        severity = alert.severity.ToString(), // Assuming it's an enum
        //        status = alert.status.ToString(),     // Assuming it's an enum
        //        date = alert.date_time.ToString("yyyy-MM-dd"),
        //        time = alert.date_time.ToString("HH:mm")
        //    });

        //    return Ok(formatted);
        //}

        [HttpGet("all")]
        public async Task<IActionResult> GetAlerts()
        {
            var alerts = await _alertServices.getAlerts(); 

            var formatted = alerts.Select(alert => new
            {
                id = alert.alert_id,
                type = alert.alert_type,
                district = alert.district,
                gnDivision = alert.ds_division,
                severity = alert.severity.ToString(),
                status = alert.status.ToString(),
                date = alert.date_time.ToString("yyyy-MM-dd"),
                time = alert.date_time.ToString("HH:mm"),
                latitude = alert.latitude,
                longitude = alert.longitude

            });

            return Ok(formatted);
        }



        [HttpGet("toResolve/{dsDivision}")]
        public async Task<IActionResult> GetAlertsByDivision(string dsDivision)
        {
            var alerts = await _alertServices.GetAlertsByDivisionAsync(dsDivision);
            return Ok(alerts);
        }

        [HttpPut("resolve/{id}")]
        public async Task<IActionResult> MarkAlertAsResolved(int id)
        {
            var success = await _alertServices.MarkAlertAsResolved(id);
            if (!success) return NotFound();
            return Ok();
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateAlert([FromBody] Alerts alert)
        {
            if (alert == null)
                return BadRequest("Invalid alert data.");

            var result = await _alertServices.CreateAlertAsync(alert);

            if (!result)
                return StatusCode(500, "Failed to create alert.");

            return Ok(new { message = "Alert created successfully." });
        }

        [HttpGet("byDistrict/{district}")]
        public async Task<IActionResult> GetAlertsByDistrict(string district)
        {
            var alerts = await _alertServices.GetAlertsByDistrictAsync(district);
            return Ok(alerts);
        }

        //[HttpGet("all")]
        //public async Task<IActionResult> GetAllAlerts()
        //{
        //    var alerts = await _alertServices.GetAllAlertsAsync();

        //    var formatted = alerts.Select(alert => new
        //    {
        //        id = alert.alert_id,
        //        type = alert.alert_type,
        //        district = alert.district,
        //        gnDivision = alert.gn_division,
        //        severity = alert.severity.ToString(), // Assuming it's an enum
        //        status = alert.status.ToString(),     // Assuming it's an enum
        //        date = alert.date_time.ToString("yyyy-MM-dd"),
        //        time = alert.date_time.ToString("HH:mm")
        //    });

        //    return Ok(formatted);
        //}




    }
}