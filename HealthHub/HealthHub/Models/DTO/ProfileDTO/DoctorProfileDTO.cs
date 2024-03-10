using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthHub.Models.DTO.ProfileDTO
{
    public class DoctorProfileDTO
    {
        public int Id { get; set; }

        public string? Desc { get; set; }
        [Display(Name = "Photo")]
        public string? PathToPhoto { get; set; }
    }
}
