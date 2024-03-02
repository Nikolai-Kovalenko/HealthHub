using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthHub.Models
{
    public class Doctor
    {
        [Key]
        public int Id { get; set; }

        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual AppUser AppUser { get; set; }

        public string? Desc { get; set; }
        public string? PathToPhoto { get; set; }
        public bool IsActive { get; set; } // Активный или заблокированый
        public DateTime? DeleteTime { get; set; }
        public DateTime RegistrationTime { get; set; }
    }
}