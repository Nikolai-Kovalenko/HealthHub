    using AutoMapper;
    using HealthHub.Models;
    using HealthHub.Models.DTO.ProfileDTO;
    using HealthHub.Models.DTO.UserDTO;
    using HealthHub.Models.ViewModel;
    using HealthHub.Repository.IRepository;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System.Data;
    using System.Runtime.Serialization;
    using System.Security.Claims;

    namespace HealthHub.Controllers
    {

        [Authorize(Roles = WC.DoctorRole)]
        public class DoctorProfileController : Controller
        {
            private readonly SignInManager<IdentityUser> _signInManager;
            private readonly RoleManager<IdentityRole> _roleManager;
            private readonly UserManager<IdentityUser> _userManager;
            private readonly IDoctorProfileRepository _doctorProfileRepository;
            public readonly IAppUserRepository _appUserRepository;
            private readonly IWebHostEnvironment _webHostEnvironment;
            public IMapper _mapper { get; set; }

            public DoctorProfileController(
                SignInManager<IdentityUser> signInManager,
                RoleManager<IdentityRole> roleManager, 
                UserManager<IdentityUser> userManager,
                IDoctorProfileRepository doctorProfileRepository,
                IMapper mapper,
                IDoctorProfileRepository profileRepository,
                IAppUserRepository appUserRepository,
                IWebHostEnvironment webHostEnvironment)
            {

                _signInManager = signInManager;
                _roleManager = roleManager;
                _userManager = userManager;
                _doctorProfileRepository = doctorProfileRepository;
                _mapper = mapper;
                _appUserRepository = appUserRepository;
                _webHostEnvironment = webHostEnvironment;
            }

            public IActionResult Index()
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity; 
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

                var doctorProfile =  _doctorProfileRepository.FirstOrDefault(u => u.UserId == claim.Value);
                AppUser appUser = _appUserRepository.FirstOrDefault(u => u.Id == claim.Value);

                DoctorProfileVM doctorProfileVM = new()
                {
                    appUserDTO = _mapper.Map<AppUserDTO>(appUser),
                    doctorProfileDTO = _mapper.Map<DoctorProfileDTO>(doctorProfile)
                };

                return View(doctorProfileVM);  
            }

            public IActionResult Upsert(int? id) // DoctorProfileDTO.id
            {
                if (id == null) // Create profile
                {
                    var claimsIdentity = (ClaimsIdentity)User.Identity;
                    var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

                    var doctorProfile = _doctorProfileRepository.FirstOrDefault(u => u.UserId == claim.Value);
                    AppUser appUser = _appUserRepository.FirstOrDefault(u => u.Id == claim.Value);

                    DoctorProfileVM doctorProfileVM = new()
                    {
                        appUserDTO = _mapper.Map<AppUserDTO>(appUser),
                        doctorProfileDTO = _mapper.Map<DoctorProfileDTO>(doctorProfile)
                    };

                    return View(doctorProfileVM);
                }

                // Update profile

                DoctorProfile objFromDb = _doctorProfileRepository.Find(id.GetValueOrDefault());

                if (objFromDb != null)
                {

                    DoctorProfileDTO doctorProfileDTO = new()
                    {
                        Desc = objFromDb.Desc,
                        PathToPhoto = objFromDb.PathToPhoto
                    };

                    return View(doctorProfileDTO);
                }

                return View();
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public IActionResult Upsert(DoctorProfileVM obj)
            {
                if (ModelState.IsValid)
                {
                    var claimsIdentity = (ClaimsIdentity)User.Identity;
                    var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

                    var objFromDb = _doctorProfileRepository.FirstOrDefault(u => u.UserId == claim.Value);

                    var curetnDate = DateTime.Now;

                    // Choose
                    if (obj.doctorProfileDTO.Id == 0)
                    {

                        DoctorProfile doctorProfile = _mapper.Map<DoctorProfile>(obj.doctorProfileDTO);
                        doctorProfile.CreateTime = curetnDate;

                        var files = HttpContext.Request.Form.Files;
                        string webRootPath = _webHostEnvironment.WebRootPath;

                        string upload = webRootPath + WC.ImagePath;
                        string fileName = Guid.NewGuid().ToString();
                        string extension = Path.GetExtension(files[0].FileName);

                        using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                        {
                            files[0].CopyTo(fileStream);
                        }

                        doctorProfile.PathToPhoto = fileName + extension;

                        doctorProfile.UserId = claim.Value;

                        _doctorProfileRepository.Add(doctorProfile);
                        _doctorProfileRepository.Save();

                        //return RedirectToAction(nameof(Index));
                    }
                    // Change
                    else
                    {
                        DoctorProfile doctorProfile = _doctorProfileRepository.FirstOrDefault(u => u.Id == obj.doctorProfileDTO.Id);
                        AppUser appUser = _appUserRepository.FirstOrDefault(u => u.Id == obj.appUserDTO.Id);

                        var uniqeEmail = _appUserRepository.FirstOrDefault(u => u.Email == obj.appUserDTO.Email);
                        if (uniqeEmail.Id == appUser.Id) 
                        {
                            if ((doctorProfile != null) && (appUser != null))
                            {
                                if (obj.doctorProfileDTO.PathToPhoto != null)
                                {
                                    var files = HttpContext.Request.Form.Files;
                                    string webRootPath = _webHostEnvironment.WebRootPath;

                                    string upload = webRootPath + WC.ImagePath;
                                    string fileName = Guid.NewGuid().ToString();
                                    string extension = Path.GetExtension(files[0].FileName);

                                    using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                                    {
                                        files[0].CopyTo(fileStream);
                                    }

                                    obj.doctorProfileDTO.PathToPhoto = fileName + extension;
                                }
                                else
                                {
                                    obj.doctorProfileDTO.PathToPhoto = objFromDb.PathToPhoto;
                                }

                                _doctorProfileRepository.Update(obj.doctorProfileDTO, curetnDate);
                                _appUserRepository.Update(obj.appUserDTO);

                                _doctorProfileRepository.Save();
                                _appUserRepository.Save();

                                return RedirectToAction(nameof(Index));
                            }
                        }

                        return View(obj);
                    }
                }

                return RedirectToAction("Index");
            }
        }
    }
