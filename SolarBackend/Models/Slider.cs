using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SolarBackend.Models
{
    public class Slider
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public string Desc { get; set; }
        [Required]
        [NotMapped]
        public IFormFile Photo { get;  set; }
    }
}
