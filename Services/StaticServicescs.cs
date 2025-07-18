using Disaster_demo.Models;
using Disaster_demo.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Disaster_demo.Services
{
    public class StatisticsService : IStatisticsService
    {
        private readonly DisasterDBContext _dbContext;

        public StatisticsService(DisasterDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> GetActiveVolunteersCountAsync()
        {
            return await _dbContext.Volunteers
                .Where(v => v.availability == AvailabilityStatus.Available)
                .CountAsync();
        }





        public async Task<int> GetTotalAlertsSentCountAsync()
        {
            // Assuming Alerts is a DbSet with all sent alerts
            return await _dbContext.Alerts.CountAsync();
        }

        

        public async Task<int> GetTotalAidRequestsCountAsync()
        {
            return await _dbContext.AidRequests.CountAsync();
        }
    }
}
