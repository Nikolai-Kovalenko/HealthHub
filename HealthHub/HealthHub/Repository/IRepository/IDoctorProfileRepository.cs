using HealthHub.Models;
using HealthHub.Models.DTO.ProfileDTO;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq.Expressions;

namespace HealthHub.Repository.IRepository
{
    public interface IDoctorProfileRepository : IRepository<DoctorProfile>
    {
        void Update(DoctorProfileDTO obj, DateTime dateTime);

        void Delete(DoctorProfile obj, DateTime dateTime);

        IEnumerable<SelectListItem> GetAllDropdownList(string obj);
    }
}
