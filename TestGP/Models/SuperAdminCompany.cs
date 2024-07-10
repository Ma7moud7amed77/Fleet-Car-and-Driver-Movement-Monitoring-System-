using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace TestGP.Models
    
{
    public class SuperAdminCompany
    {
        
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public int MyComp_id { get; set; }
        public string MyComp_Name { get; set; }
        public string MyComp_Location { get; set; }
        public string MyComp_Phone { get; set; }
        public string? Img { get; set; }

        [JsonIgnore]
        public virtual List<Manager>?Managers { get; set; }




    }
}
