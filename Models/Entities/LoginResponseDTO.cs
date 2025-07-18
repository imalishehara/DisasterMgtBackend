namespace Disaster_demo.Models.Entities
{
    //public class LoginResponseDTO
    //{
    //    public int UserId { get; set; }     
    //    public UserRole Role { get; set; }
    //    public string Message { get; set; }
    //    public string? GnDivision { get; set; }



    //}

    public class LoginResponseDTO
    {
        public int UserId { get; set; }
        public string Role { get; set; }   // changed from UserRole to string
        public string Message { get; set; }
        public string? DsDivision { get; set; } // optional

        public string FullName { get; set; }
        public string ContactNo { get; set; }
        public string District { get; set; }

        public string Email { get; set; }
        public AvailabilityStatus? Availability { get; set; }
    }

}
