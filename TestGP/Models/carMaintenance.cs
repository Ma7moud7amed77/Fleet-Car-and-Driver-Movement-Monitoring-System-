using System.Text.Json.Serialization;

namespace TestGP.Models
{
    
    public class carMaintenance
    {
        public int id { get; set; }

        [ForeignKey("Car")]
        public int carId { get; set; }
        [JsonIgnore]
        public Car? Car { get; set; }
        public DateTime maintenanceDay { get; set; }
    }
}
