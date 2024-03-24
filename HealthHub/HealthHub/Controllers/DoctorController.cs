using AutoMapper;
using HealthHub.Data;
using HealthHub.Models;
using HealthHub.Models.DTO.PatientDoctorRelationDTO;
using HealthHub.Models.DTO.ProfileDTO;
using HealthHub.Models.DTO.UserDTO;
using HealthHub.Models.ViewModel;
using HealthHub.Repository;
using HealthHub.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Security.Claims;

namespace HealthHub.Controllers
{
    [Authorize(Roles = WC.UserRole)]
    public class DoctorController : Controller
    {

        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IPatientDoctorRelationRepository _patientDoctorRelationRepo;
        public readonly IAppUserRepository _appUserRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IDoctorProfileRepository _doctorProfileRepository;
        private readonly AppDbContext _db;

        public IMapper _mapper { get; set; }


        public DoctorController(
            SignInManager<IdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            UserManager<IdentityUser> userManager,
            IDoctorProfileRepository doctorProfileRepository,
            IMapper mapper,
            IPatientDoctorRelationRepository patientDoctorRelationRepo,
            IAppUserRepository appUserRepository,
            IWebHostEnvironment webHostEnvironment,
            AppDbContext db
            )
        {
            _signInManager = signInManager;
            _roleManager = roleManager;
            _userManager = userManager;
            _patientDoctorRelationRepo = patientDoctorRelationRepo;
            _mapper = mapper;
            _appUserRepository = appUserRepository;
            _webHostEnvironment = webHostEnvironment;
            _doctorProfileRepository = doctorProfileRepository;
            _db = db;
        }

        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            var patientDoctorRelation = _patientDoctorRelationRepo.FirstOrDefault(u => u.UserId == claim.Value);

            DoctorProfile doctorProfile;
            AppUser doctorCommonData;

            if (patientDoctorRelation == null)
            {
                doctorProfile = _doctorProfileRepository.FirstOrDefault(u => u.UserId == null);
                doctorCommonData = _appUserRepository.FirstOrDefault(u => u.Id == null);
            }
            else
            {
                doctorProfile = _doctorProfileRepository.FirstOrDefault(u => u.UserId == patientDoctorRelation.DoctorId);
                doctorCommonData = _appUserRepository.FirstOrDefault(u => u.Id == patientDoctorRelation.DoctorId);
            }

            DoctorProfileVM doctorProfileVM = new()
            {
                appUserDTO = _mapper.Map<AppUserDTO>(doctorCommonData),
                doctorProfileDTO = _mapper.Map<DoctorProfileDTO>(doctorProfile)
            };

            return View(doctorProfileVM);
        }

        public IActionResult Upsert()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var relationFromDb = _patientDoctorRelationRepo.FirstOrDefault(u => u.UserId == claim.Value);

            if (relationFromDb == null) // Choose doctor
            {
                ChooseDoctorVM chooseDoctorVM = new()
                {
                    patientDoctorRelation = relationFromDb,
                    doctorProfileList = _doctorProfileRepository.GetAllDropdownList(WC.DoctorList)
                };

                return View(chooseDoctorVM);
            }

            // change doctor

            if (relationFromDb != null)
            {

                ChooseDoctorVM chooseDoctorVM = new()
                {
                    patientDoctorRelation = relationFromDb,
                    doctorProfileList = _doctorProfileRepository.GetAllDropdownList(WC.DoctorList)
                };

                return View(chooseDoctorVM);
            }

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ChooseDoctorVM obj)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            var objFromDb = _patientDoctorRelationRepo.FirstOrDefault(u => u.UserId == claim.Value);
            var doctorProfile = _doctorProfileRepository.FirstOrDefault(u => u.Id == Convert.ToInt32(obj.patientDoctorRelation.DoctorId));
            
            if (objFromDb == null) // Choose
            {
                PatientDoctorRelation patientDoctorRelation = new()
                {
                    UserId = claim.Value,
                    DoctorId = doctorProfile.UserId
                };

                _patientDoctorRelationRepo.Add(patientDoctorRelation);
                _patientDoctorRelationRepo.Save();

                return RedirectToAction(nameof(Index));
            }


            PatientDoctorRelationDTO patientDoctorRelationDTO = new()
            {
                Id = obj.patientDoctorRelation.Id,
                DoctorId = doctorProfile.UserId,
                UserId = claim.Value
            };
            _patientDoctorRelationRepo.Update(patientDoctorRelationDTO);
            _patientDoctorRelationRepo.Save();

            return RedirectToAction("Index");
        }

    }
}
