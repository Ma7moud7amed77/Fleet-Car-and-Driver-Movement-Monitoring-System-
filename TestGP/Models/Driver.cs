using System.Text.Json.Serialization;

namespace TestGP.Models
{
    public class Driver
    {

        public int Id { get; set; }
        public int Health_statusd { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string License { get; set; }
        public DateTime LicenseExpDate { get; set; }
        public string? Img { get; set; }
        public int Salary { get; set; }
        [ForeignKey("Manager")]
        public int? MgrID { get; set; }
        //relation bwteen manager and Driver 'Add'
        [JsonIgnore]
        public Manager Manager { get; set; }
        //Relation one Admin Add  Many Driver
        [ForeignKey("Admin")]
        public int? AdminID { get; set; }
        [JsonIgnore]
        public Admin? Admin { get; set; }
        // one to many relationship between Ass and Driver 'Add'
        [ForeignKey("Assistant")]
        public int? AssID { get; set; }
        [JsonIgnore]
        public Assistant? Assistant { get; set; }

        //Relation one Company Work with Many Drivers
        [ForeignKey("Company")]
        public int? CompID { get; set; }
        [JsonIgnore]
        public Company? Company { get; set; }

        //one to many optional Ass Communicate Driver
        [JsonIgnore]
        public List<DriverCommunicateAss>? DriverCommunicateAss { get; set; }

        //tinary relationship between car ,driver ,locatio that manage the violation
        [JsonIgnore]
        public List<ManageViolation>? ManageViolation { get; set; } 



    }
}