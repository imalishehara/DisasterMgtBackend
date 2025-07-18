using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Disaster_demo.Models.Entities
{
    public class Symptoms
    {
        [Key]
        public int report_id { get; set; }

        public DateTime date_time { get; set; }

        public string reporter_name { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be exactly 10 digits")]
        [StringLength(10, ErrorMessage = "Phone number must be exactly 10 digits")]
        public string contact_no { get; set; }

        public string district { get; set; }

        public string ds_division { get; set; }

        public string description { get; set; }

        public string image { get; set; }

        public string action { get; set; } = "Pending";
        public double Latitude { get; set; }  
        public double Longitude { get; set; }
    }
}


