using HealthHub.Models;
using HealthHub.Models.DTO.MedCardDTO;
using HealthHub.Models.DTO.ProfileDTO;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq.Expressions;

namespace HealthHub.Repository.IRepository
{
    public interface IMedCardRepository : IRepository<MedCard>
    {
        void Update(MedCardDTO obj, DateTime dateTime);

        void Delete(DoctorProfile obj, DateTime dateTime);

        IEnumerable<SelectListItem> GetAllDropdownList(string obj);
    }
}
