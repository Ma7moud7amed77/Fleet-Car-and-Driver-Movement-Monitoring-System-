using TestGP.Models;

namespace TestGP.DTO
{
    public class postCardto
    {
        public string Model { get; set; }
        public string Name { get; set; }
        public string License { get; set; }
        public DateTime LicensExpDate { get; set; }
        public int? CompID { get; set; }
        public int? driverId { get; set; }
        public int? AdminId { get; set; }
        public int? AssId { get; set; }
        public IFormFile? Img { get; set; }
    }
}