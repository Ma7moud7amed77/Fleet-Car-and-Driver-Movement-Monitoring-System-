using System.Text.Json.Serialization;

namespace TestGP.Models
{
    public class ReportData
    {
        [Key]
        public int Id { get; set; }

        // Foreign key properties
        [ForeignKey("Driver")]
        public int DriverId { get; set; }
        public Driver Driver { get; set; }
        [ForeignKey("Violation")]
        public int ViolationId { get; set; }
        public Violation Violation { get; set; }
        [ForeignKey("Assistant")]
        public int AssistantId { get; set; }
        public Assistant Assistant { get; set; }
        [ForeignKey("Admin")]
        public int AdminId { get; set; }
        public Admin Admin { get; set; }
        [ForeignKey("Car")]
        public int CarId { get; set; }
        public Car Car { get; set; }
       
        

        public DateTime ReportTime { get; set; }

        
    }

}
