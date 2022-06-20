using Microsoft.EntityFrameworkCore;
using SolarBackend.Models;

namespace SolarBackend.DAL
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions options):base(options)
        {

        }
        public DbSet<Slider>Sliders { get; set; }
        public DbSet<About>About { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Gallery>Galleries { get; set; }
        public DbSet<GalleryPhoto> GalleryPhotos { get; set; }
        public DbSet<Faq>Faqs { get; set; }
        public DbSet<Client>Clients { get; set; }
        public DbSet<Bio> Bios { get; set; }
    }
}
