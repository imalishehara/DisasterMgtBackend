using Disaster_demo.Models.Entities;

namespace Disaster_demo.Services
{
    public interface ISymptomsServices
    {
        Task createSymptoms(Symptoms symptoms);
        Task<List<Symptoms>> GetDsApprovedSymptomsByDistrictAsync(string district);

        Task<List<Symptoms>> GetPendingReportsByDivisionAsync(string dsDivision);
        List<Symptoms> GetPendingSymptomsByDsDivision(string dsDivision);
        bool UpdateSymptomStatus(int reportId, string status);
    }
}