using Disaster_demo.Models.Entities;

namespace Disaster_demo.Services
{
    public interface IVolunteerServices
    {
        Task<LoginResponseDTO?> GetVolunteerDetailsAsync(string userId);
        Task<List<Volunteer>> GetVolunteersByGnDivisionAsync(string dsDivision);
        Task<int> SignupAsync(VolunteerSignupDTO dto);
        Task<List<Volunteer>> GetVolunteersByDistrictAsync(string district);
        Task<bool> UpdateAvailabilityAsync(int userId, AvailabilityStatus newStatus);


    }
}