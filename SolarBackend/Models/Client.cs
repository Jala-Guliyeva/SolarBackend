using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SolarBackend.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string Image { get; set; }
        [Required]
        [NotMapped]
        public IFormFile Photo{ get; set; }
        public string Desc { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
    }
}
