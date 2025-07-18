using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Disaster_demo.Models.Entities
{
    [Table("contributions")]
    public class Contribution
    {
        [Key]
        public int contribution_id { get; set; }
        public int volunteer_id { get; set; }
        public string district { get; set; }
        public string type_support { get; set; }
        public string description { get; set; }
        public DateTime created_at { get; set; } = DateTime.UtcNow;
    }
}
