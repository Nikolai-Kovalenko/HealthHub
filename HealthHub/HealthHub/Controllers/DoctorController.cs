using AutoMapper;
using HealthHub.Models;
using HealthHub.Models.DTO.ProfileDTO;
using HealthHub.Models.DTO.UserDTO;
using HealthHub.Models.ViewModel;
using HealthHub.Repository;
using HealthHub.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
        public IMapper _mapper { get; set; }


        public DoctorController(
            SignInManager<IdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            UserManager<IdentityUser> userManager,
            IDoctorProfileRepository doctorProfileRepository,
            IMapper mapper,
            IPatientDoctorRelationRepository patientDoctorRelationRepo,
            IAppUserRepository appUserRepository,
            IWebHostEnvironment webHostEnvironment)
        {
            _signInManager = signInManager;
            _roleManager = roleManager;
            _userManager = userManager;
            _patientDoctorRelationRepo = patientDoctorRelationRepo;
            _mapper = mapper;
            _appUserRepository = appUserRepository;
            _webHostEnvironment = webHostEnvironment;
            _doctorProfileRepository = doctorProfileRepository;
        }

        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            // тут кося в том, вто мне передаёться не объект, а null, а потом я у этого null ищу параметр.
            // Нужно бы искать не всеь объект, а сам парамерт. Ну, или сделать проверку на null, и если объект не найден,
            // то изменить логику нахождения doctorProfile и doctorCommonData

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
        
    }
}
