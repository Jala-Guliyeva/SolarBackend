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
    public class ClientController : Controller
    {
        private AppDbContext _context;
        private IWebHostEnvironment _env;
        public ClientController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            List<Client> clients = _context.Clients.ToList();
            return View(clients);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken] //jsda yazsaqbunusilmeliyik,yoxsaislemiir
        public async Task<IActionResult> Create(Client client)
        {
            //validationstate-requiredolanlar
            if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
            {
                return View();

            }

            if (!client.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "Accept only image!");

                return View();
            }
            if (client.Photo.ImageSize(10000))
            {
                ModelState.AddModelError("Photo", "1mq yuxari olabilmez!");

                return View();
            }
            //string path = @"C:\Users\TOSHIBA\Desktop\FiorelloAdminF\FiorelloTask\wwwroot\img\";

            string fileName = await client.Photo.SaveImage(_env, "img");
            Client newClient = new Client();
            newClient.Image = fileName;
            await _context.Clients.AddAsync(newClient);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return NotFound();
            Client dbClient = await _context.Clients.FindAsync(id);
            if (dbClient == null) return NotFound();
            return View(dbClient);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            Client dbClient = await _context.Clients.FindAsync(id);
            if (dbClient == null) return NotFound();
            Helper.DeleteFile(_env, "img", dbClient.Image);
            _context.Clients.Remove(dbClient);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");


        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) return NotFound();

            Client dbClient = await _context.Clients.FindAsync(id);
            if (dbClient == null) return NotFound();

            return View(dbClient);



        }
        [HttpPost]
        public async Task<IActionResult> Update(int? id, Client client)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            Client dbClient = await _context.Clients.FindAsync(id);

            if (dbClient == null) return NotFound();
            dbClient.Desc = client.Desc;
            dbClient.Name = client.Name;
            dbClient.Author = client.Author;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
