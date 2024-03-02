using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthHub.Models
{
    public class Appointment
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("UserId")]
        //[InverseProperty("UserId")]
        public virtual AppUser FK_UserId { get; set; }
        public string UserId { get; set; }

        [ForeignKey("DoctorId")]
        //[InverseProperty("DoctorId")]
        public virtual AppUser FK_DoctorId { get; set; }
        public string DoctorId { get; set; }

        public DateTime AppointmentDate { get; set; }
        public DateTime? CancellationDate { get; set; }
    }
}
