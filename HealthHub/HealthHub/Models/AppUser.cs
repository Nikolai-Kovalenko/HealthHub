using Microsoft.AspNetCore.Identity;

namespace HealthHub.Models
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Sex { get; set; }
        public DateTime Birthday { get; set; }
        //public string? PathToPhoto { get; set; }
        public bool IsActive { get; set; } // Активный или заблокированый
        public DateTime? DeleteTime { get; set; }
        public DateTime RegistrationTime { get; set; }
    }
}