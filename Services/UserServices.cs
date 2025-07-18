using Disaster_demo.Models;
using Disaster_demo.Models.Entities;
using Microsoft.EntityFrameworkCore; 

namespace Disaster_demo.Services
{
    public class UserServices : IUserServices
    {
        private readonly DisasterDBContext _dbContext;

        public UserServices(DisasterDBContext dbContext)
        {
            this._dbContext = dbContext;
        }

        

        public async Task<LoginResponseDTO?> LoginAsync(LoginDTO dto)
        {
            var user = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.user_id == dto.UserId && u.password == dto.Password);

            if (user == null)
                return null;

            string roleStr = user.role switch
            {
                UserRole.DS => "DS",
                UserRole.DMC => "DMC",
                UserRole.volunteer => "volunteer",
                _ => ""
            };

            var response = new LoginResponseDTO
            {
                UserId = user.user_id,
                Role = user.role.ToString(),
                Message = "Login successful"
            };

            if (user.role == UserRole.DS)
            {
                var dsOfficer = await _dbContext.DS_Officers
                    .FirstOrDefaultAsync(g => g.user_id == user.user_id);

                if (dsOfficer != null)
                {
                    response.DsDivision = dsOfficer.ds_division;
                    response.FullName = dsOfficer.name;
                    response.ContactNo = dsOfficer.contact_no;
                    response.District = dsOfficer.district;
                }
            }
            else if (user.role == UserRole.DMC)
            {
                var dmcOfficer = await _dbContext.DMCOfficers.FirstOrDefaultAsync(d => d.user_id == user.user_id);
                return new LoginResponseDTO
                {
                    UserId = user.user_id,
                    Role = "DMC",
                    Message = "Login successful",
                    FullName = dmcOfficer?.name,         // Make sure this is filled
                    ContactNo = dmcOfficer?.contact_no,  // And this
                    District = dmcOfficer?.district       // And this
                };
            }


            //else if (user.role == UserRole.volunteer)
            //{
            //    var volunteer = await _dbContext.Volunteers
            //        .FirstOrDefaultAsync(v => v.user_id == user.user_id);

            //    if (volunteer != null)
            //    {
            //        response.GnDivision = volunteer.gn_division; 
            //    }
            //}

            else if (user.role == UserRole.volunteer)
            {
                var volunteer = await _dbContext.Volunteers
                    .FirstOrDefaultAsync(v => v.user_id == user.user_id);

                if (volunteer != null)
                {
                    response.DsDivision = volunteer.ds_division;
                    response.FullName = volunteer.name;
                    response.ContactNo = volunteer.email; // Can remain as contact
                    response.District = volunteer.district;
                    response.Availability = volunteer.availability;
                }
            }


            return response;
        }

    }
}