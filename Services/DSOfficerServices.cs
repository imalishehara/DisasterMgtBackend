using Disaster_demo.Models;
using Disaster_demo.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Disaster_demo.Services
{
    public class DSOfficerServices : IDSOfficerServices
    {
        private readonly DisasterDBContext _dbContext;

        public DSOfficerServices(DisasterDBContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<LoginResponseDTO?> GetDsOfficerDetailsAsync(string userId)
        {
            if (!int.TryParse(userId, out int parsedUserId))
                return null; // Invalid ID format

            var officer = await _dbContext.DS_Officers
                .FirstOrDefaultAsync(g => g.user_id == parsedUserId);

            if (officer == null) return null;

            return new LoginResponseDTO
            {
                UserId = officer.user_id,
                FullName = officer.name,
                ContactNo = officer.contact_no,
                District = officer.district,
                DsDivision = officer.ds_division,
                Role = "DS Officer",
                Message = "DS Officer details fetched successfully"
            };
        }


    }
}
