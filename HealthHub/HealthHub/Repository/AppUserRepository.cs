using HealthHub.Data;
using HealthHub.Models;
using HealthHub.Models.DTO.UserDTO;
using HealthHub.Repository.IRepository;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HealthHub.Repository
{
    public class AppUserRepository<T> : Repository<AppUser>, IAppUserRepository
    {
        private readonly AppDbContext _db;

        public AppUserRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public void Delete(AppUser obj, DateTime dateTime)
        {
            obj.DeleteTime = dateTime;
        }

        public IEnumerable<SelectListItem> GetAllDropdownList(string obj)
        {
            if (obj == WC.DoctorList)
            {
                // TODO
                // Сейчас выбираеться только имя доктора. Нужно подумать о выводе описания и фото доктора
                // А вообще, на так уж фотка в этом моменте и нужна. Доктора можно и без фотки выбрать :)
                return _db.DoctorsProfile.Where(i => i.DeleteTime == null).Select(i => new SelectListItem
                {
                    Text = i.AppUser.UserName,
                    Value = i.Id.ToString()
                });
            }

            return null;
        }

        public void Update(AppUserDTO obj)
        {
            var objFromDb = base.FirstOrDefault(u => u.Id == obj.Id);
            if (objFromDb != null)
            {
                objFromDb.Name = obj.Name;
                objFromDb.Surname = obj.Surname;   
                objFromDb.Email = obj.Email;
                objFromDb.UserName = obj.Email;
                objFromDb.NormalizedUserName = obj.Email.ToUpper();
                objFromDb.NormalizedEmail = obj.Email.ToUpper();
                objFromDb.Birthday = obj.Birthday;
            }
        }
    }
}
