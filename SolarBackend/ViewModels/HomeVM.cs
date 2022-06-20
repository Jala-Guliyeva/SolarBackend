using SolarBackend.Models;
using System.Collections.Generic;

namespace SolarBackend.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<Slider>sliders { get; set; }
        public IEnumerable<Service> services { get; set; }
        public About abouts { get; set; }
        public Gallery galleries { get; set; }
        public IEnumerable<GalleryPhoto>galleryPhotos { get; set; }
        public IEnumerable<Faq>faqs { get; set; }
        public Client clients { get; set; }
        public Bio bios { get; set; }
    }
}
