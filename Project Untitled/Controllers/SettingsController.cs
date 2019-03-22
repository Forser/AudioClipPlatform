using Microsoft.AspNetCore.Mvc;

namespace Project_Untitled.Controllers
{
    public class SettingsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}