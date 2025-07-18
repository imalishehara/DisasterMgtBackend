using Disaster_demo.Models;
using Disaster_demo.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace Disaster_demo.Services
{
    public class AlertServices : IAlertServices
    {
        private readonly DisasterDBContext _dbContext;

        public AlertServices(DisasterDBContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<List<Alerts>> getAlerts()
        {
            var alerts = await _dbContext.Alerts
                .Where(a => a.status == AlertStatus.Ongoing)
                .ToListAsync();
            return alerts;
        }

        public async Task<List<Alerts>> GetAlertsByDivisionAsync(string gnDivision)
        {
            return await _dbContext.Alerts
                .Where(a => a.status == AlertStatus.Ongoing && a.ds_division.ToLower() == gnDivision.ToLower())
                .ToListAsync();
        }

        public async Task<bool> MarkAlertAsResolved(int id)
        {
            var alert = await _dbContext.Alerts.FindAsync(id);
            if (alert == null) return false;

            alert.status = AlertStatus.Resolved;
            await _dbContext.SaveChangesAsync();
            return true;
        }


        public async Task<bool> CreateAlertAsync(Alerts alert)
        {
            try
            {
                alert.date_time = DateTime.Now;
                alert.status = AlertStatus.Ongoing;

                _dbContext.Alerts.Add(alert);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }


        public async Task<List<Alerts>> GetAlertsByDistrictAsync(string district)
        {
            return await _dbContext.Alerts
                .Where(a => a.status == AlertStatus.Ongoing && a.district.ToLower() == district.ToLower())
                .ToListAsync();
        }


        public async Task<List<Alerts>> GetAllAlertsAsync()
        {
            return await _dbContext.Alerts
                .OrderByDescending(a => a.date_time)
                .ToListAsync();
        }


       


    }
}
