using Microsoft.AspNetCore.Mvc;
using SolarBackend.DAL;
using SolarBackend.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SolarBackend.ViewComponents
{
    public class FooterViewComponent:ViewComponent
    {
        private AppDbContext _context;
        public FooterViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            Bio bio = _context.Bios.FirstOrDefault();
            return View(await Task.FromResult(bio));
        }

    
    }
}
