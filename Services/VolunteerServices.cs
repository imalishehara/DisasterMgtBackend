using Disaster_demo.Models;
using Disaster_demo.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Disaster_demo.Services;
using System.Numerics;

namespace Disaster_demo.Services
{
    public class VolunteerServices : IVolunteerServices
    {
        private readonly DisasterDBContext _dbContext;

        public VolunteerServices(DisasterDBContext dbContext)
        {
            this._dbContext = dbContext;
        }

        //public async Task<string> SignupAsync(VolunteerSignupDTO dto)
        //{
        //    if (_dbContext.Users.Any(u => u.email == dto.Email))
        //    {
        //        return "Email already registered.";
        //    }

        //    // No need to parse the enum since the DTO already uses the correct type
        //    var volunteer = new Volunteer
        //    {
        //        email = dto.Email,
        //        password = dto.Password,
        //        role = UserRole.volunteer,
        //        status = UserStatus.active,
        //        name = dto.Name,
        //        district = dto.District,
        //        gn_division = dto.GnDivision,
        //        availability = AvailabilityStatus.Unavailable // Use the enum value directly
        //    };

        //    _dbContext.Volunteers.Add(volunteer);
        //    await _dbContext.SaveChangesAsync();

        //    return "Signup successful.";
        //}


        public async Task<int> SignupAsync(VolunteerSignupDTO dto)
        {
            if (_dbContext.Users.Any(u => u.email == dto.Email))
            {
                return -1; // Indicate email already registered
            }

            var volunteer = new Volunteer
            {
                email = dto.Email,
                password = dto.Password,
                role = UserRole.volunteer,
                status = UserStatus.active,
                name = dto.Name,
                district = dto.District,
                ds_division = dto.DsDivision,
                availability = AvailabilityStatus.Unavailable
            };

            _dbContext.Volunteers.Add(volunteer);
            await _dbContext.SaveChangesAsync();

            return volunteer.user_id;  // Return the generated user_id (assuming auto-increment)
        }




        //public async Task<List<Volunteer>> GetAllVolunteersAsync()
        //{
        //    return await _dbContext.Volunteers.ToListAsync();
        //}

        public async Task<List<Volunteer>> GetVolunteersByGnDivisionAsync(string dsDivision)
        {
            return await _dbContext.Volunteers
                .Where(v => v.ds_division == dsDivision)
                .ToListAsync();
        }


        public async Task<LoginResponseDTO?> GetVolunteerDetailsAsync(string userId)
        {
            if (!int.TryParse(userId, out int parsedUserId))
                return null;

            var volunteer = await _dbContext.Volunteers
                .FirstOrDefaultAsync(v => v.user_id == parsedUserId);

            if (volunteer == null) return null;

            return new LoginResponseDTO
            {
                UserId = volunteer.user_id,
                FullName = volunteer.name,
                DsDivision = volunteer.ds_division,
                Role = "Volunteer",
                Message = "Volunteer details fetched successfully",
                ContactNo=volunteer.email,
                District = volunteer.district,
                Availability = volunteer.availability
            };
        }

        public async Task<List<Volunteer>> GetVolunteersByDistrictAsync(string district)
        {
            return await _dbContext.Volunteers
                .Where(v => v.district == district)
                .ToListAsync();
        }

        public async Task<bool> UpdateAvailabilityAsync(int userId, AvailabilityStatus newStatus)
        {
            var volunteer = await _dbContext.Volunteers.FirstOrDefaultAsync(v => v.user_id == userId);
            if (volunteer == null)
                return false;

            volunteer.availability = newStatus;
            await _dbContext.SaveChangesAsync();
            return true;
        }





    }
}
