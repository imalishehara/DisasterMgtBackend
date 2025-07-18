using Disaster_demo.Models.Entities;

namespace Disaster_demo.Services
{
    public interface IAlertServices
    {
        Task<bool> CreateAlertAsync(Alerts alert);
        Task<List<Alerts>> getAlerts();
        Task<List<Alerts>> GetAlertsByDistrictAsync(string district);
        Task<List<Alerts>> GetAlertsByDivisionAsync(string dsDivision);
        Task<bool> MarkAlertAsResolved(int id);
        //Task<List<Alerts>> GetAllAlertsAsync();

      
    }
}