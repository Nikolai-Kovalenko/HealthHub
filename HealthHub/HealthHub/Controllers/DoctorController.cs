using HealthHub.Models;
using HealthHub.Models.DTO.ProfileDTO;
using HealthHub.Repository;
using HealthHub.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HealthHub.Controllers
{
    public class DoctorController : Controller
    {       

        public IActionResult Index()
        {
            return View();
        }

        
    }
}
