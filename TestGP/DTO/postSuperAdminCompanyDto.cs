namespace TestGP.DTO
{
    public class postSuperAdminCompanyDto
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public int MyComp_id { get; set; }
        public string MyComp_Name { get; set; }
        public string MyComp_Location { get; set; }
        public string MyComp_Phone { get; set; }
        public IFormFile? Img { get; set; }

    }
}
