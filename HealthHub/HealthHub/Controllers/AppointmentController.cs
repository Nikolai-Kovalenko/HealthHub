using Microsoft.AspNetCore.Mvc;

namespace HealthHub.Controllers
{
    public class AppointmentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
