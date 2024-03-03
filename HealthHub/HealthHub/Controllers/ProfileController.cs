using AutoMapper;
using HealthHub.Models.DTO.ProfileDTO;
using HealthHub.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;

namespace HealthHub.Controllers
{
    public class ProfileController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IDoctorProfileRepository _doctorProfileRepository;

        public IMapper _mapper { get; set; }

        public ProfileController(
            SignInManager<IdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager, 
            UserManager<IdentityUser> userManager,
            IDoctorProfileRepository doctorProfileRepository,
            IMapper mapper)
        {

            _signInManager = signInManager;
            _roleManager = roleManager;
            _userManager = userManager;
            _doctorProfileRepository = doctorProfileRepository;
            _mapper = mapper;
        }

        [Authorize(Roles = WC.DoctorRole)]
        public IActionResult DoctorProfile()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            var doctorProfile =  _doctorProfileRepository.FirstOrDefault(u => u.UserId == claim.Value);

            var doctorProfileDTO = _mapper.Map<DoctorProfileDTO>(doctorProfile);

            return View(doctorProfileDTO);  
        }

        [Authorize(Roles = WC.UserRole)]
        public IActionResult UserProfile() {
            return View();
        }
    }
}
