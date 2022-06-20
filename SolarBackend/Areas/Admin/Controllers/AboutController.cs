using FiorelloTask.Extentions;
using FiorelloTask.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using SolarBackend.DAL;
using SolarBackend.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SolarBackend.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AboutController : Controller
    {
        private readonly AppDbContext _context;

        private IWebHostEnvironment _env;
        public AboutController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            List<About> about = _context.About.ToList();
            return View(about);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken] //jsda yazsaqbunusilmeliyik,yoxsaislemiir
        public async Task<IActionResult> Create(About about)
        {
            //validationstate-requiredolanlar
            if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
            {
                return View();

            }

            if (!about.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "Accept only image!");

                return View();
            }
            if (about.Photo.ImageSize(10000))
            {
                ModelState.AddModelError("Photo", "1mq yuxari olabilmez!");

                return View();
            }
            //string path = @"C:\Users\TOSHIBA\Desktop\FiorelloAdminF\FiorelloTask\wwwroot\img\";

            string fileName = await about.Photo.SaveImage(_env, "img");
            About newAbout = new About();
            newAbout.Image = fileName;
            await _context.About.AddAsync(newAbout);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return NotFound();
            About dbAbout = await _context.About.FindAsync(id);
            if (dbAbout == null) return NotFound();
            return View(dbAbout);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            About dbAbout = await _context.About.FindAsync(id);
            if (dbAbout == null) return NotFound();
            Helper.DeleteFile(_env, "img", dbAbout.Image);
            _context.About.Remove(dbAbout);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");


        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) return NotFound();

            About dbAbout = await _context.About.FindAsync(id);
            if (dbAbout == null) return NotFound();

            return View(dbAbout);



        }
        [HttpPost]
        public async Task<IActionResult> Update(int? id, About about)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            About dbAbout = await _context.About.FindAsync(id);

            if (dbAbout == null) return NotFound();
            dbAbout.Title = about.Title;
            dbAbout.Desc = about.Desc;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
