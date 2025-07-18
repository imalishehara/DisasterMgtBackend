using Disaster_demo.Models;
using Disaster_demo.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Disaster_demo.Services;
using System.Numerics;

namespace Disaster_demo.Services
{
    public class AidRequestServices : IAidRequestServices
    {
        private readonly DisasterDBContext _dbContext;

        public AidRequestServices(DisasterDBContext dbContext)
        {
            this._dbContext = dbContext;
        }
        

        public async Task<List<AidRequests>> getPendingAidRequests(string divisionalSecretariat)
        {
            var approved = await _dbContext.AidRequests
                .Where(s => s.dsApprove == DsApprovalStatus.Pending && s.divisional_secretariat == divisionalSecretariat)
                .OrderByDescending(s => s.date_time)
                .ToListAsync();

            return approved;
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

        //public async Task createAidRequest(AidRequests aidrequests)
        //{
        //    this._dbContext.AidRequests.Add(aidrequests);
        //    await this._dbContext.SaveChangesAsync();


        //}

        public async Task<bool> CreateAidRequestAsync(AidRequests request)
        {
            if (!Enum.IsDefined(typeof(AidRequestType), request.request_type))
            {
                return false;  // Invalid enum value
            }

            request.date_time = DateTime.UtcNow;

            _dbContext.AidRequests.Add(request);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        //public List<Symptoms> GetPendingSymptoms()
        //{
        //    return _dbContext.Symptoms
        //        .Where(s => s.gn_approve == "Pending")
        //        .OrderByDescending(s => s.date_time)
        //        .ToList();
        //}


        //public bool UpdateSymptomStatus(int reportId, string status)
        //{
        //    Console.WriteLine($"Updating Report ID {reportId} to status: {status}");

        //    var symptom = _dbContext.Symptoms.FirstOrDefault(s => s.report_id == reportId);

        //    if (symptom == null)
        //    {
        //        Console.WriteLine("Symptom not found.");
        //        return false;
        //    }

        //    symptom.action = status;
        //    Console.WriteLine($"Before Save: {symptom.action}");

        //    _dbContext.SaveChanges();  // Make sure this is hit

        //    Console.WriteLine("SaveChanges called");
        //    return true;
        //}



        //public bool UpdateStatus(StatusUpdateModel model)
        //{
        //    var aidRequest = _dbContext.AidRequests.FirstOrDefault(a => a.aid_id == model.ReportId);
        //    if (aidRequest == null)
        //        return false;

        //    if (string.Equals(model.Actor, "GN", StringComparison.OrdinalIgnoreCase))
        //    {
        //        if (Enum.TryParse<GnApprovalStatus>(model.Status, true, out var parsedGnStatus))
        //        {
        //            aidRequest.gnApprove = parsedGnStatus;
        //            _dbContext.SaveChanges();
        //            return true;
        //        }
        //    }
        //    else if (string.Equals(model.Actor, "DMC", StringComparison.OrdinalIgnoreCase))
        //    {
        //        if (Enum.TryParse<DmcApprovalStatus>(model.Status, true, out var parsedDmcStatus))
        //        {
        //            aidRequest.dmcApprove = parsedDmcStatus;
        //            _dbContext.SaveChanges();
        //            return true;
        //        }
        //    }

        //    return false;
        //}

        public bool UpdateStatus(StatusUpdateModel model)
        {
            var aidRequest = _dbContext.AidRequests.FirstOrDefault(a => a.aid_id == model.ReportId);
            if (aidRequest == null)
                return false;

            if (string.Equals(model.Actor, "DS", StringComparison.OrdinalIgnoreCase))
            {
                if (Enum.TryParse<DsApprovalStatus>(model.Status, true, out var parsedGnStatus))
                {
                    aidRequest.dsApprove = parsedGnStatus;
                    _dbContext.SaveChanges();
                    return true;
                }
            }
            else if (string.Equals(model.Actor, "DMC", StringComparison.OrdinalIgnoreCase))
            {
                if (Enum.TryParse<DmcApprovalStatus>(model.Status, true, out var parsedDmcStatus))
                {
                    aidRequest.dmcApprove = parsedDmcStatus;


                    if (parsedDmcStatus == DmcApprovalStatus.Approved)
                    {
                        var dsOfficer = _dbContext.DS_Officers
                            .FirstOrDefault(g => g.divisional_secretariat.ToLower() == aidRequest.divisional_secretariat.ToLower());

                        if (dsOfficer != null)
                        {
                            aidRequest.assign_ds = dsOfficer.user_id;
                        }
                    }

                    _dbContext.SaveChanges();
                    return true;
                }
            }

            return false;
        }




        public async Task<List<AidRequests>> GetAidRequestsByDistrict(string district)
        {
            var requests = await _dbContext.AidRequests
                .Where(a => a.district.ToLower() == district.ToLower()
                            && a.dsApprove == DsApprovalStatus.Approved
                            && a.dmcApprove == DmcApprovalStatus.Pending)
                .OrderByDescending(a => a.date_time)
                .ToListAsync();

            return requests;
        }

        //public async Task<List<AidRequests>> GetDmcApprovedAidRequests()
        //{
        //    var approvedRequests = await _dbContext.AidRequests
        //        .Where(a => a.gnApprove == GnApprovalStatus.Approved && a.dmcApprove == DmcApprovalStatus.Approved)
        //        .OrderByDescending(a => a.date_time)
        //        .ToListAsync();

        //    return approvedRequests;
        //}

        public async Task<List<AidRequests>> GetDmcApprovedAidRequests()
        {
            var approvedRequests = await _dbContext.AidRequests
                
                .Where(a => a.dsApprove == DsApprovalStatus.Approved && a.dmcApprove == DmcApprovalStatus.Approved)
                .OrderByDescending(a => a.date_time)
                .ToListAsync();

            return approvedRequests;
        }



        public async Task<List<AidRequests>> GetAllDmcRelatedAidRequests(string district)
        {
            var result = await _dbContext.AidRequests
                .Where(a =>
                    a.district.ToLower() == district.ToLower() &&
                    a.dsApprove == DsApprovalStatus.Approved &&
                    (a.dmcApprove == DmcApprovalStatus.Pending || a.dmcApprove == DmcApprovalStatus.Approved))
                .OrderByDescending(a => a.date_time)
                .ToListAsync();

            return result;
        }




    }
}
