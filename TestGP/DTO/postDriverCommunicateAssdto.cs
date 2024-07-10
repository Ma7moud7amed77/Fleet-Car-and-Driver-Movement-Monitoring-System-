namespace TestGP.DTO
{
    public class postDriverCommunicateAssdto
    {
        public int AssID { get; set; }
        public int DriverID { get; set; }
        public DateTime DateOfMessageAss { get; set; }
        public DateTime DateOfMessageDriver { get; set; }
        public string? DriverComment { get; set; }
        public string? AssComment { get; set; }
    }
}
