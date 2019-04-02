using Microsoft.AspNetCore.Mvc;
using ProjectUntitled.Models;
using ProjectUntitled.Models.ViewModels;
using System.Linq;

namespace ProjectUntitled.Controllers
{
    public class HomeController : Controller
    {
        private readonly IClipsRepository clipsRepository;
        public int PageSize = 10;

        public HomeController(IClipsRepository clipsRepo)
        {
            clipsRepository = clipsRepo;
        }

        public ViewResult Index(int clipPage = 1) => View(new ClipsViewModel
        {
            Clips = clipsRepository.Clips
            .Where(s => s.FileStatus == FileStatus.Listed)
            .OrderByDescending(o => o.UploadAt)
            .Skip((clipPage - 1) * PageSize)
            .Take(PageSize),
            PagingInfo = new PagingInfo {
                CurrentPage = clipPage,
                ItemsPerPage = PageSize,
                TotalItems = clipsRepository.Clips.Where(s => s.FileStatus == FileStatus.Listed).Count()
            }
        });
    }
}