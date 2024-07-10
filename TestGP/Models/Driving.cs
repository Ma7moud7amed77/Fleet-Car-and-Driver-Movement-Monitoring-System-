using System.Text.Json.Serialization;

namespace TestGP.Models
{
    [PrimaryKey("CarID","DriverID","date")]
    public class Driving
    {
        [ForeignKey("Car")]
        public int CarID { get; set; }
        [JsonIgnore]
        public Car Car { get; set; }
        [ForeignKey("Driver")]
        public int DriverID { get; set; }
        [JsonIgnore]
        public Driver Driver{ get; set; }
        public DateTime date { get; set; }
    }
}
