using Microsoft.AspNetCore.Mvc;
using Project_Untitled.Models;
using System.Threading.Tasks;

namespace Project_Untitled.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IProfileRepository _profileRepository;

        public ProfileController(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetProfile(string profileName)
        {
            var profile = await _profileRepository.GetProfile(profileName);

            return View(profile);
        }

        public IActionResult Settings()
        {
            return View();
        }
    }
}