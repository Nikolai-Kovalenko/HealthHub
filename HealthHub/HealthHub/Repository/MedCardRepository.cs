using HealthHub.Data;
using HealthHub.Models;
using HealthHub.Models.DTO.MedCardDTO;
using HealthHub.Models.DTO.ProfileDTO;
using HealthHub.Repository.IRepository;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HealthHub.Repository
{
    public class MedCardRepository : Repository<MedCard>, IMedCardRepository
    {
        private readonly AppDbContext _db;

        public MedCardRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }


        public void Delete(DoctorProfile obj, DateTime dateTime)
        {
            obj.DeleteTime = dateTime;
        }

        public IEnumerable<SelectListItem> GetAllDropdownList(string obj)
        {
            if (obj == WC.CardList)
            {
                return _db.DoctorsProfile.Where(i => i.DeleteTime == null).Select(i => new SelectListItem
                {
                    Text = i.AppUser.UserName,
                    Value = i.Id.ToString()
                });
            }

            return null;
        }


        public void Update(MedCardDTO obj, DateTime dateTime)
        {
            var objFromDb = base.FirstOrDefault(u => u.Id == obj.Id);
            if (objFromDb != null)
            {
                objFromDb.Desc = obj.Desc;
                objFromDb.ChangeUserId = obj.ChangeUserId;
                objFromDb.ChangeDate = dateTime;
            }
        }
    }
}
