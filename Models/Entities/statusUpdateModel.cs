namespace Disaster_demo.Models.Entities
{
    public class StatusUpdateModel
    {
        public int ReportId { get; set; }
        public string Status { get; set; }
        public string Actor { get; set; }

        public int? UserId { get; set; }
    }
}
