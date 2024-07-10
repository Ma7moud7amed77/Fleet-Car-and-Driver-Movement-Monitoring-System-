namespace TestGP.DTO
{
    public class postApplicantdto
    {
        public string Name { get; set; }
        public string Position { get; set; }
        public int Age { get; set; }
        public string? License { get; set; }
        public DateTime  LicenseExpDate { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? Address { get; set; }
        public int? CompId { get; set; }
        public IFormFile? Img { get; set; }


    }
}
