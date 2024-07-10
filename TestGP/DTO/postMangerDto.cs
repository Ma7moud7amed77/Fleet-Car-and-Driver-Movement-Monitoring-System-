namespace TestGP.DTO
{
    public class postMangerDto
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string Subcribtion { get; set; }
        public DateTime DateOfSubscribtion { get; set; }
        public int? SuperAdminId { get; set; }
        public IFormFile? Img { get; set; }

    }
}
