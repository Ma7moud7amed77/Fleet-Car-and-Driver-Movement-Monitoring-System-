using System.Text.Json.Serialization;

namespace TestGP.Models
{
    public class Manager
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string Subcribtion { get; set; }
        public string? Img { get; set; }

        public DateTime DateOfSubscribtion { get; set; }
        //Relation between Super Admin and manager 
        [ForeignKey("SuperAdminCompany")]
        public int? SuperAdminId { get; set; }
        [JsonIgnore]
        public SuperAdminCompany? SuperAdminCompany { get; set; }
        //Manager has many company
        [JsonIgnore]
        public virtual List<Company>? Companies { get; set; }
        // Add relationship with assistanat
        [JsonIgnore]
        public virtual List<Assistant>? Assistants { get; set; }
        // Add relationship with Driver
        [JsonIgnore]
        public virtual List<Driver>? Drivers { get; set; }
        // Add relationship with Admins
        [JsonIgnore]
        public virtual List<Admin>? Admins { get; set; }







    }
}
