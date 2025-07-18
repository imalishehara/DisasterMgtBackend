using Disaster_demo.Models;
using Disaster_demo.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Disaster_demo.Services
{
    public class DMCOfficerServices : IDMCOfficerServices
    {
        private readonly DisasterDBContext _context;

        public DMCOfficerServices(DisasterDBContext context)
        {
            _context = context;
        }

        public async Task<LoginResponseDTO?> GetDMCOfficerDetailsAsync(string userId)
        {
            var officer = await _context.DMCOfficers
                .FirstOrDefaultAsync(o => o.user_id.ToString() == userId);

            if (officer == null) return null;

            return new LoginResponseDTO
            {
                UserId = officer.user_id,
                FullName = officer.name,
                District = officer.district,
                Role = "dmc",
                Message = "Login successful"
            };
        }

    }

}
