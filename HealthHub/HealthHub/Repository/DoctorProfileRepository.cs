using HealthHub.Data;
using HealthHub.Models;
using HealthHub.Repository.IRepository;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HealthHub.Repository
{
    public class DoctorProfileRepository : Repository<DoctorProfile>, IDoctorProfileRepository
    {
        private readonly AppDbContext _db;

        public DoctorProfileRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }


        public void Delete(DoctorProfile obj, DateTime dateTime)
        {
            obj.DeleteTime = dateTime;
        }

        public IEnumerable<SelectListItem> GetAllDropdownList(string obj)
        {
            if (obj == WC.DoctorList)
            {
                // TODO
                // Сейчас выбираеться только имя доктора. Нужно подумать о выводе описания и фото доктора
                return _db.DoctorsProfile.Where(i => i.DeleteTime == null).Select(i => new SelectListItem
                {
                    Text = i.AppUser.UserName,
                    Value = i.Id.ToString()
                });
            }

            return null;
        }


        /*public void Update(TopicUpsertDTO obj, DateTime dateTime)
        {
            var objFromDb = _db.Category.FirstOrDefault(u => u.Id == obj.Id);
            var objFromDb = base.FirstOrDefault(u => u.Id == obj.Id);
            if (objFromDb != null)
            {
                objFromDb.Name = obj.Name;
                objFromDb.Description = obj.Description;
                objFromDb.LastChangeTime = dateTime;
                objFromDb.SectionId = obj.SectionId;
            }
        }*/
    }
}
