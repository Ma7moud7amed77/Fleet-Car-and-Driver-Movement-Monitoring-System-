using System.Text.Json.Serialization;

namespace TestGP.Models
{
      [PrimaryKey("DriverId", "date")]
    public class CurrentLocation
    {
        public string lat { get; set; }
        public string lon { get; set; }
        public int CurrentSpeed { get; set; }
        public DateTime date { get; set; }
        [ForeignKey("Driver")]
        public int? DriverId { get; set; }
        [JsonIgnore]
        public Driver? Driver { get; set; }
    }
}
