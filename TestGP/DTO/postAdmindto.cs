using TestGP.Models;

namespace TestGP.DTO
{
    public class postAdmindto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

        public int Salary { get; set; }
        public int? CompID { get; set; }
        public int? MgrID { get; set; }
        public IFormFile? Img { get; set; }
    }
}
