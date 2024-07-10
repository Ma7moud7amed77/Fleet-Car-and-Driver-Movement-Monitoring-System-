using System.Text.Json.Serialization;

namespace TestGP.Models
{
    //[PrimaryKey("AssID", "DriverID", "DateOfMessageAss", "AssComment")]
    public class DriverCommunicateAss
    {
        public int Id { get; set; }

        [ForeignKey("Assistant")]
        public int AssID { get; set; }
        [ForeignKey("Driver")]
        public int DriverID { get; set; }
        public DateTime DateOfMessageAss { get; set; }
        public DateTime DateOfMessageDriver { get; set; }
        public string? DriverComment { get; set; }
        public string? AssComment { get; set; }
        [JsonIgnore]
        public Assistant? Assistant { get; set; }
        [JsonIgnore]
        public Driver? Driver { get; set; }
    }
}
