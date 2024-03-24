using HealthHub.Models.DTO.PatientDoctorRelationDTO;
using HealthHub.Models.DTO.ProfileDTO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HealthHub.Models.ViewModel
{
    public class ChooseDoctorVM
    {
        public PatientDoctorRelation patientDoctorRelation { get; set; }
        public IEnumerable<SelectListItem>? doctorProfileList { get; set; }
    }
}
