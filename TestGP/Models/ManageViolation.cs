using System.Text.Json.Serialization;

namespace TestGP.Models
{
    [PrimaryKey("DriverID","ViolationID")]
    public class ManageViolation
    {
        [ForeignKey("Driver")]
        public int DriverID { get; set; } = 1;
        [JsonIgnore]
        public Driver Driver { get; set; }
        public DateTime DateOfViolation { get; set; }

       /* [ForeignKey("Location")]
        public int? LocID { get; set; }
        [JsonIgnore]
        public Location? Location { get; set; }
       */ public string lat  { get; set; }
        public string lon  { get; set; }


        [ForeignKey("Car")]
        public int? CarID { get; set; } = 1;
        [JsonIgnore]
        public Car? Car { get; set; }
        public int CurrentSpeed { get; set; }
        [ForeignKey("Violation")]
        public int ?ViolationID { get; set; }
        [JsonIgnore]
        public Violation? Violation { get; set; }








    }
}
