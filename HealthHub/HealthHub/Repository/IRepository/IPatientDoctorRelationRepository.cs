using HealthHub.Models;
using HealthHub.Models.DTO.PatientDoctorRelationDTO;
using HealthHub.Models.DTO.ProfileDTO;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq.Expressions;

namespace HealthHub.Repository.IRepository
{
    public interface IPatientDoctorRelationRepository : IRepository<PatientDoctorRelation>
    {
        void Update(PatientDoctorRelationDTO obj);

        IEnumerable<SelectListItem> GetAllDropdownList(string obj);
    }
}
