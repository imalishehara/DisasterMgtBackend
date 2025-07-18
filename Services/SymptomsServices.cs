using Disaster_demo.Models;
using Disaster_demo.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Disaster_demo.Services;
using System.Numerics;

namespace Disaster_demo.Services
{
    public class SymptomsServices : ISymptomsServices
    {
        private readonly DisasterDBContext _dbContext;

        public SymptomsServices(DisasterDBContext dbContext)
        {
            this._dbContext = dbContext;
        }

        //public async Task<List<Symptoms>> getPendingSymptoms()
        //{
        //    var symptoms = await _dbContext.Symptoms
        //        .Where(s => s.action == "Pending")
        //        .ToListAsync();

        //    return symptoms;
        //}

        public async Task<List<Symptoms>> GetPendingReportsByDivisionAsync(string dsDivision)
        {
            return await _dbContext.Symptoms
                .Where(r => r.action == "Pending" && r.ds_division == dsDivision)
                .ToListAsync();
        }

        //public async Task<bool> ApproveSymptom(int id)
        //{
        //    var symptom = await _dbContext.Symptoms.FindAsync(id);
        //    if (symptom == null) return false;

        //    symptom.action = "Approved";


        //    await _dbContext.SaveChangesAsync();
        //    return true;
        //}

        //public async Task<Symptoms> SubmitSymptomsAsync(Symptoms symptoms)
        //{
        //    _dbContext.Symptoms.Add(symptoms);
        //    await _dbContext.SaveChangesAsync();
        //    return symptoms;
        //}

        public async Task createSymptoms(Symptoms symptoms)
        {
            this._dbContext.Symptoms.Add(symptoms);
            await this._dbContext.SaveChangesAsync();


        }


        //public List<Symptoms> GetPendingSymptoms()
        //{
        //    return _dbContext.Symptoms
        //        .Where(s => s.action == "Pending")
        //        .OrderByDescending(s => s.date_time)
        //        .ToList();
        //}

        public List<Symptoms> GetPendingSymptomsByDsDivision(string dsDivision)
        {
            return _dbContext.Symptoms
                .Where(s => s.action == "Pending" && s.ds_division == dsDivision)
                .OrderByDescending(s => s.date_time)
                .ToList();
        }


        //public async Task<List<Symptoms>> GetPendingSymptomsByGnDivision(string gnDivision)
        //{
        //    return await _dbContext.Symptoms
        //        .Where(s => s.gn_approve == GnApprovalStatus.Pending && s.gn_division == gnDivision)
        //        .OrderByDescending(s => s.date_time)
        //        .ToListAsync();
        //}






        public bool UpdateSymptomStatus(int reportId, string status)
        {
            Console.WriteLine($"Updating Report ID {reportId} to status: {status}");

            var symptom = _dbContext.Symptoms.FirstOrDefault(s => s.report_id == reportId);

            if (symptom == null)
            {
                Console.WriteLine("Symptom not found.");
                return false;
            }

            symptom.action = status;
            Console.WriteLine($"Before Save: {symptom.action}");

            _dbContext.SaveChanges();  // Make sure this is hit

            Console.WriteLine("SaveChanges called");
            return true;
        }

        public async Task<List<Symptoms>> GetDsApprovedSymptomsByDistrictAsync(string district)
        {
            return await _dbContext.Symptoms
                .Where(s => s.action == "Approved" && s.district == district)
                .ToListAsync();
        }



    }
}

 
