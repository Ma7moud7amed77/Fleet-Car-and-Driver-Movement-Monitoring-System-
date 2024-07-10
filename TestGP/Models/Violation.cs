using System.Text.Json.Serialization;

namespace TestGP.Models
{
    public class Violation
    {
        public int Id { get; set; }
        public string type { get; set; }
        public DateTime? date { get; set; }
        [ForeignKey("driver")]
        public int? driverID { get; set; }
        [JsonIgnore]
        public Driver? driver { get; set; }
        public string? Img { get; set; }
      /*  [JsonIgnore]
        public List<ManageViolation>? ManageViolation { get; set; }
      */  
        /*public string lat { get; set; }
        public string lon { get; set; }*/
        public int ?CurrentSpeed { get; set; }
        public float ?Load { get; set; }
        public int ?RPM { get; set; }
        [ForeignKey("Car")]
        public int? CarID { get; set; } = 1;
        [JsonIgnore]
        public Car? Car { get; set; }

    }
}
