using Microsoft.AspNetCore.Mvc;

namespace HealthHub.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
