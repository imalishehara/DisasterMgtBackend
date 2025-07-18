using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Disaster_demo.Models.Entities
{
    [Table("ds_officers")]
    public class DS_Officer : Users
    {
        //[Key]
        public int ds_id { get; set; }
        public string name { get; set; }
        public string contact_no { get; set; }
        public string district { get; set; }
        public string ds_division { get; set; }
        
        
    }
}
