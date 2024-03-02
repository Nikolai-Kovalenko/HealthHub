using Microsoft.AspNetCore.Mvc;

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
