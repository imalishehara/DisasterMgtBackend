using System.ComponentModel.DataAnnotations;

namespace Disaster_demo.Models.Entities
{
    public class Alerts
    {
        [Key]
        public int alert_id { get; set; }
        public string alert_type { get; set; }
        public string district { get; set; }
        public string ds_division { get; set; }
        public SeverityLevel severity { get; set; }
        public DateTime date_time { get; set; }
        public AlertStatus status { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
    }
}
