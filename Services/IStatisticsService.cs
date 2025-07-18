
namespace Disaster_demo.Services
{
    public interface IStatisticsService
    {
        Task<int> GetActiveVolunteersCountAsync();
       
        Task<int> GetTotalAidRequestsCountAsync();
        Task<int> GetTotalAlertsSentCountAsync();
    }
}