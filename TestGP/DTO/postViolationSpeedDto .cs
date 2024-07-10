using System.Text.Json.Serialization;
using TestGP.Models;

namespace TestGP.DTO
{
    public class postViolationSpeedDto
    {
        public string type { get; set; } 
        public DateTime? date { get; set; }

        public int? driverID = 1;
        public int CurrentSpeed { get; set; }
        public int? CarID { get; set; } = 2;
        public float? Load { get; set; }
        public int? RPM { get; set; }




    }
}