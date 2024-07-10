using System.Text.Json.Serialization;
using TestGP.Models;

namespace TestGP.DTO
{
    public class postViolationdto
    {
        public string type { get; set; }
        public DateTime? date { get; set; }
        public int? driverID = 1;
        public int CarId { get; set; } = 2;
        public IFormFile? Img { get; set; }

    }
}