using System.Text.Json.Serialization;

namespace TestGP.Models
{
   
    public class AssCommunicateAdmin
    {
        public int Id { get; set; }
        [ForeignKey("Assistant")]
        public int? AssID { get; set; }
        [ForeignKey("Admin")]
        public int? AdminID { get; set; }
        public DateTime? DateOfMessageAdmin { get; set; }
        public DateTime? DateOfMessageAss { get; set; }
        public string? AdminComment { get; set; }
        public string? AssComment { get; set; }
        [JsonIgnore]
        public Assistant? Assistant { get; set; }
        [JsonIgnore]
        public Admin? Admin { get; set; }

    }
}
