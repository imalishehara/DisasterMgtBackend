using Disaster_demo.Services;
using Microsoft.AspNetCore.Mvc;

namespace Disaster_demo.Controllers
{
    public class StatisticsController : ControllerBase
    {
        private readonly IStatisticsService _statisticsService;

        public StatisticsController(IStatisticsService statisticsService)
        {
            _statisticsService = statisticsService;
        }

       
        [HttpGet("active-volunteers-count")]
        public async Task<IActionResult> GetActiveVolunteersCount()
        {
            var count = await _statisticsService.GetActiveVolunteersCountAsync();
            return Ok(new { count });
        }

        [HttpGet("alerts-sent-count")]
        public async Task<IActionResult> GetAlertsSentCount()
        {
            var count = await _statisticsService.GetTotalAlertsSentCountAsync();
            return Ok(new { count });
        }

        [HttpGet("total-aid-requests-count")]
        public async Task<IActionResult> GetTotalAidRequestsCount()
        {
            var count = await _statisticsService.GetTotalAidRequestsCountAsync();
            return Ok(new { count });
        }

    }
}
