using Disaster_demo.Models.Entities;

namespace Disaster_demo.Services
{
    public interface IAidRequestServices
    {
        //Task createAidRequest(AidRequests aidrequests);
        Task<bool> CreateAidRequestAsync(AidRequests request);
        Task<List<AidRequests>> GetAidRequestsByDistrict(string district);
        Task<List<AidRequests>> GetAllDmcRelatedAidRequests(string district);
        Task<List<AidRequests>> GetDmcApprovedAidRequests();
        Task<List<AidRequests>> getPendingAidRequests(string dsDivision);
        bool UpdateStatus(StatusUpdateModel model);
        
    }
}