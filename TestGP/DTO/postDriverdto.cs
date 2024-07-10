using TestGP.Models;

namespace TestGP.DTO
{
    public class postDriverdto
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Health_statusd { get; set; }
        public string Phone { get; set; }
        public string License { get; set; }
        public DateTime LicenseExpDate { get; set; }
        public int Salary { get; set; }
        public int? MgrID { get; set; }
        public int? AdminID { get; set; }
        public int? AssID { get; set; }
        public int? CompID { get; set; }
        public IFormFile? Img { get;    set; }
    }
}
