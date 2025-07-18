using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Disaster_demo.Models.Entities
{
    [Table("dmc_officers")]
    public class DMCOfficer: Users
    {
       
        public int dmc_id { get; set; }
        public string name { get; set; }
        public string contact_no { get; set; }
        public string district { get; set; }
    }
}
