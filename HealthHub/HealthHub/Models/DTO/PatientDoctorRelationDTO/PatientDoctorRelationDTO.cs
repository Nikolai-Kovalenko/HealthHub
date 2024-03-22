using System.ComponentModel.DataAnnotations.Schema;

namespace HealthHub.Models.DTO.PatientDoctorRelationDTO
{
    public class PatientDoctorRelationDTO
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public string DoctorId { get; set; }
    }
}
