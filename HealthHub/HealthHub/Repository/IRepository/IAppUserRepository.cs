using HealthHub.Models;
using HealthHub.Models.DTO.UserDTO;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq.Expressions;

namespace HealthHub.Repository.IRepository
{
    public interface IAppUserRepository : IRepository<AppUser>
    {
        void Update(AppUserDTO obj);

        void Delete(AppUser obj, DateTime dateTime);

        IEnumerable<SelectListItem> GetAllDropdownList(string obj);
    }
}
