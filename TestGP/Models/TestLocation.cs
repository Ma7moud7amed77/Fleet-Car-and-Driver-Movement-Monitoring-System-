using System.Text.Json.Serialization;

namespace TestGP.Models
{
    public class TestLocation
    {
        public int Id { get; set; }
        public string lat { get; set; }
        public string lon { get; set; }
        [ForeignKey("Car")]
        public int CarId { get; set; } 
        [JsonIgnore]
        public Car? Car { get; set; }
        public DateTime? date { get; set; }

    }
}
