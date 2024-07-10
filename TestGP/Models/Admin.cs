using System.Text.Json.Serialization;

namespace TestGP.Models
{
    public class Admin
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string? Img { get; set; }
        public int Salary{ get; set; }
        // one to one relationship between Company and Admin

        [ForeignKey("Company")]
        public int? CompID { get; set; }
        [JsonIgnore]
        public Company? Company { get; set; }

        [ForeignKey("Manager")]
        public int? MgrID { get; set; }
        //relation bwteen manager and Admin 'Add'
        [JsonIgnore]
        public Manager? Manager { get; set; }
        //relation bwteen Admin and Assistant 'Add'
        [JsonIgnore]
        public List<Assistant>? Assistants { get; set; }
        //relation bwteen Admin and Drivers 'Add'
        [JsonIgnore]
        public List<Driver>? Drivers { get; set; }
        //one to many optional Ass Communicate Admin
        [JsonIgnore]
        public List<AssCommunicateAdmin>? AssCommunicateAdmin { get; set; }



    }
}
