using Disaster_demo.Models.Entities;

namespace Disaster_demo.Services
{
    public interface IDMCOfficerServices
    {
        Task<LoginResponseDTO?> GetDMCOfficerDetailsAsync(string userId);
    }
}