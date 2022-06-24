using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SolarBackend.DAL;
using SolarBackend.Models;

namespace SolarBackend.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BiosController : Controller
    {
        private readonly AppDbContext _context;

        public BiosController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Bios.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bio = await _context.Bios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bio == null)
            {
                return NotFound();
            }

            return View(bio);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Location,Number,Email,Author")] Bio bio)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bio);
        }

        
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bio = await _context.Bios.FindAsync(id);
            if (bio == null)
            {
                return NotFound();
            }
            return View(bio);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, [Bind("Id,Location,Number,Email,Author")] Bio bio)
        {
            if (id != bio.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BioExists(bio.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(bio);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bio = await _context.Bios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bio == null)
            {
                return NotFound();
            }

            return View(bio);
        }


        private bool BioExists(int id)
        {
            return _context.Bios.Any(e => e.Id == id);
        }
    }
}
