using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthHub.Models
{
    public class PatientDoctorRelation
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("UserId")]
        public virtual AppUser FK_UserId { get; set; }
        public string UserId { get; set; }

        [ForeignKey("DoctorId")]
        public virtual AppUser FK_DoctorId { get; set; }
        public string DoctorId { get; set; }
    }
}
