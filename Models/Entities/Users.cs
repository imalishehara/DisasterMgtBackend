using System.ComponentModel.DataAnnotations;

namespace Disaster_demo.Models.Entities
{
    public class Users
    {
        [Key]
        public int user_id { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public UserRole role { get; set; }
        public UserStatus? status { get; set; } = UserStatus.active;





    }
}
