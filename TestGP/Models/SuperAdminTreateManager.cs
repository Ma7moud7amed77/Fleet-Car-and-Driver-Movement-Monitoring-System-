using System.Text.Json.Serialization;

namespace TestGP.Models
{ 
  //  [PrimaryKey("SuperID", "MgrID")]
    public class SuperAdminTreateManager
    {
        public int Id { get; set; }

        [ForeignKey("SuperAdminCompany")]
        public int? SuperID { get; set; }
        [JsonIgnore]
        public SuperAdminCompany? SuperAdminCompany { get; set; }
        [ForeignKey("Manager")]
        public int? MgrID { get; set; }
        [JsonIgnore]
        public Manager? Manager { get; set; } 
        public string? MgrFeedback { get; set; }
        public string? SuperFeedback { get; set; }
        public DateTime? DateOfMgrFeedback { get; set; }
        public DateTime? DateOfSuperFeedback { get; set; }

    }
}
