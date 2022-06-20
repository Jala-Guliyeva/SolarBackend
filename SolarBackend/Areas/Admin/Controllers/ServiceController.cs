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
    public class ServiceController : Controller
    {
        private readonly AppDbContext _context;

        private IWebHostEnvironment _env;
        public ServiceController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            List<Service> service = _context.Services.ToList();
            return View(service);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken] //jsda yazsaqbunusilmeliyik,yoxsaislemiir
        public async Task<IActionResult> Create(Service service)
        {
            //validationstate-requiredolanlar
            if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
            {
                return View();

            }

            if (!service.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "Accept only image!");

                return View();
            }
            if (service.Photo.ImageSize(10000))
            {
                ModelState.AddModelError("Photo", "1mq yuxari olabilmez!");

                return View();
            }
            //string path = @"C:\Users\TOSHIBA\Desktop\FiorelloAdminF\FiorelloTask\wwwroot\img\";

            string fileName = await service.Photo.SaveImage(_env, "img");
            Service newService = new Service();
            newService.Image = fileName;
            await _context.Services.AddAsync(newService);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return NotFound();
            Service dbService = await _context.Services.FindAsync(id);
            if (dbService == null) return NotFound();
            return View(dbService);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            Service dbService = await _context.Services.FindAsync(id);
            if (dbService == null) return NotFound();
            Helper.DeleteFile(_env, "img", dbService.Image);
            _context.Services.Remove(dbService);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");


        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) return NotFound();

            Service dbService = await _context.Services.FindAsync(id);
            if (dbService == null) return NotFound();

            return View(dbService);



        }
        [HttpPost]
        public async Task<IActionResult> Update(int? id,  Service service)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            Service dbService = await _context.Services.FindAsync(id);

            if (dbService == null) return NotFound();
            dbService.Title = service.Title;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}

