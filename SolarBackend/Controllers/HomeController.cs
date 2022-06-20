using Microsoft.AspNetCore.Mvc;
using SolarBackend.DAL;
using SolarBackend.ViewModels;
using System.Linq;

namespace SolarBackend.Controllers
{
    public class HomeController : Controller
    {
        private AppDbContext _context;
        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            HomeVM homeVM = new HomeVM();
            homeVM.sliders = _context.Sliders.ToList();
            homeVM.abouts = _context.About.FirstOrDefault();
            homeVM.services = _context.Services.ToList();
            homeVM.galleries = _context.Galleries.FirstOrDefault();
            homeVM.galleryPhotos = _context.GalleryPhotos.ToList();
            homeVM.faqs = _context.Faqs.ToList();
            homeVM.clients = _context.Clients.FirstOrDefault();
            return View(homeVM);
        }
    }
}
