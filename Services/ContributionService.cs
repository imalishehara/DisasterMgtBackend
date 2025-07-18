// Services/ContributionService.cs
using Disaster_demo.Models;
using Disaster_demo.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Disaster_demo.Services
{
    public class ContributionService : IContributionService
    {
        private readonly DisasterDBContext _dbContext;

        public ContributionService(DisasterDBContext context)
        {
            _dbContext = context;
        }

        public async Task<bool> AddContributionAsync(ContributionDTO dto)
        {
            var contribution = new Contribution
            {
                volunteer_id = dto.volunteer_id,
                district = dto.district,
                type_support = dto.type_support,
                description = dto.description
            };

            _dbContext.Contribution.Add(contribution);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<List<Contribution>> GetContributionsByVolunteerIdAsync(int volunteerId)
        {
            return await _dbContext.Contribution
                .Where(c => c.volunteer_id == volunteerId)
                .OrderByDescending(c => c.created_at)
                .ToListAsync();
        }

        public async Task<int> GetTotalContributionsCountAsync(int volunteerId)
        {
            return await _dbContext.Contribution
                .CountAsync(c => c.volunteer_id == volunteerId);
        }

        public async Task<Contribution> GetLatestContributionAsync(int volunteerId)
        {
            return await _dbContext.Contribution
                .Where(c => c.volunteer_id == volunteerId)
                .OrderByDescending(c => c.created_at)
                .FirstOrDefaultAsync();
        }


    }
}