using System.Text.Json.Serialization;

namespace TestGP.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public string Name { get; set; }
        public string License { get; set; }
        public DateTime LicensExpDate { get; set; }
        // one to many relationship Company has many cars
        [ForeignKey("Company")]
        public int? CompID { get; set; }
        [JsonIgnore]
        public Company? Company { get; set; }
        //one to one relationship driver drive car
       
        [ForeignKey("Assistant")]
        public int? AssId { get; set; }
        [JsonIgnore]
        public Assistant? Assistant { get; set; } 
         [ForeignKey("Admin")]
        public int? AdminId { get; set; }
        [JsonIgnore]
        public Admin? Admin{ get; set; }

        [ForeignKey("Manager")]
        public int? MgrID { get; set; }
        [JsonIgnore]

        public Manager? Manager { get; set; }
        //tinary relationship between car ,driver ,locatio that manage the violation
        [JsonIgnore]
        public List<Violation>? violations{ get; set; }
        [JsonIgnore]
        public List<carMaintenance>? carMaintenances { get; set; }
        public string? Img { get; set; }





    }
}