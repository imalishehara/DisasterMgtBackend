using Disaster_demo.Models.Entities;

namespace Disaster_demo.Services
{
    public interface IUserServices
    {
        Task<LoginResponseDTO?> LoginAsync(LoginDTO dto);
    }
}