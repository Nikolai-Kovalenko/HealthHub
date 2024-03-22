using HealthHub.Data;
using HealthHub.Models;
using HealthHub.Models.DTO.MedCardDTO;
using HealthHub.Models.DTO.PatientDoctorRelationDTO;
using HealthHub.Models.DTO.ProfileDTO;
using HealthHub.Repository.IRepository;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HealthHub.Repository
{
    public class PatientDoctorRelationRepository : Repository<PatientDoctorRelation>, IPatientDoctorRelationRepository
    {
        private readonly AppDbContext _db;

        public PatientDoctorRelationRepository(AppDbContext db) : base(db)
        {
            _db = db;
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


        public void Update(PatientDoctorRelationDTO obj)
        {
            var objFromDb = base.FirstOrDefault(u => u.Id == obj.Id);
            if (objFromDb != null)
            {
                objFromDb.UserId = obj.UserId;
                objFromDb.DoctorId = obj.DoctorId;
            }
        }
    }
}
