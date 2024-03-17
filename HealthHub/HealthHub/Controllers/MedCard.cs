using HealthHub.Models.DTO.ProfileDTO;
using HealthHub.Models.DTO.UserDTO;
using HealthHub.Models.ViewModel;
using HealthHub.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using HealthHub.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using HealthHub.Models.DTO.MedCardDTO;

namespace HealthHub.Controllers
{
    public class MedCard : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        public readonly IAppUserRepository _appUserRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public IMedCardRepository _medCardRepo { get; set; }

        public IMapper _mapper { get; set; }

        public MedCard(
            SignInManager<IdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            UserManager<IdentityUser> userManager,
            IMapper mapper,
            IDoctorProfileRepository profileRepository,
            IAppUserRepository appUserRepository,
            IWebHostEnvironment webHostEnvironment,
            IMedCardRepository medCardRepo)
        {
            _signInManager = signInManager;
            _roleManager = roleManager;
            _userManager = userManager;
            _mapper = mapper;
            _appUserRepository = appUserRepository;
            _webHostEnvironment = webHostEnvironment;
            _medCardRepo = medCardRepo;
        }

        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            var medCard = _medCardRepo.FirstOrDefault(u => u.UserId == claim.Value);
            AppUser appUser = _appUserRepository.FirstOrDefault(u => u.Id == claim.Value);

            MedCardVM medCardVM = new()
            {
                appUserDTO = _mapper.Map<AppUserDTO>(appUser),
                medCardDTO = _mapper.Map<MedCardDTO>(medCard)
            };

            return View(medCardVM);
        }
    }
}
