using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Disaster_demo.Models.Entities
{
    [Table("aid_requests")]
    public class AidRequests
    {

        [Key]
        public int aid_id { get; set; }
        public DateTime date_time { get; set; }
        public string full_name { get; set; }

        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be 10 digits")]
        public string contact_no { get; set; }
        public string district { get; set; }
        public string divisional_secretariat { get; set; }
        public string type_support { get; set; }
        public int family_size { get; set; }
        public string description { get; set; }

        [Column("ds_approve")]
        [JsonPropertyName("dsApprove")]
        public DsApprovalStatus dsApprove { get; set; } = DsApprovalStatus.Pending;

        [Column("dmc_approve")]
        public DmcApprovalStatus dmcApprove { get; set; }
        public int? assign_ds { get; set; }
        public AidRequestType request_type { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
    }
}


