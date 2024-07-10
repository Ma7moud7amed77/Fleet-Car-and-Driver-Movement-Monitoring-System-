using System.Text.Json.Serialization;

namespace TestGP.Models
{
    public class Assistant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public int Salary { get; set; }
        public string? Img { get; set; }
        //Relation one Admin Add  Many Assistant
        [ForeignKey("Admin")]
        public int? AdminID { get; set; }
        [JsonIgnore]
        public Admin? Admin { get; set; }

        //Relation one Company Work with Many Assistant
        [ForeignKey("Company")]
        public int? CompID { get; set; }
        [JsonIgnore]
        public Company? Company { get; set; }

        [ForeignKey("Manager")]
        public int? MgrID { get; set; }
        //relation bwteen manager and Assistant 'Add'
        [JsonIgnore]
        public Manager? Manager { get; set; }
        [JsonIgnore]
        public List<AssCommunicateAdmin>? AssCommunicateAdmin { get; set; }

        //one to many optional Ass Communicate Driver
        [JsonIgnore]
        public List<DriverCommunicateAss>? DriverCommunicateAss { get; set; }
        //one Assistant add many Drivers
        [JsonIgnore]
        public List<Driver>? Drivers { get; set; }



    }
}
