// Services/ContributionService.cs
using Disaster_demo.Models.Entities;

namespace Disaster_demo.Services
{
    public interface IContributionService
    {
        Task<bool> AddContributionAsync(ContributionDTO dto);
        Task<List<Contribution>> GetContributionsByVolunteerIdAsync(int volunteerId);
        Task<Contribution> GetLatestContributionAsync(int volunteerId);
        Task<int> GetTotalContributionsCountAsync(int volunteerId);
    }
}