namespace TestGP.Models
{
    [PrimaryKey("carId", "obd2Violation")]
    public class carObd2Violation
    {
        [ForeignKey("Car")]
        public int carId { get; set; }
        public Car Car { get; set; }
        public string obd2Violation { get; set; }
    }
}
