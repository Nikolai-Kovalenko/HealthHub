using System.ComponentModel.DataAnnotations.Schema;

namespace HealthHub.Models.DTO.ProfileDTO
{
    public class DoctorProfileDTO
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual AppUser AppUser { get; set; }

        public string? Desc { get; set; }
        public string? PathToPhoto { get; set; }
    }
}
