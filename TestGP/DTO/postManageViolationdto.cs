using TestGP.Models;

namespace TestGP.DTO
{
    public class postManageViolationdto
    {
        public int DriverID { get; set; } = 1;
        public DateTime DateOfViolation { get; set; }
        //public int LocID { get; set; }
        //public int CarID { get; set; }
        public int CurrentSpeed { get; set; }
        //public int? ViolationID { get; set; }
        public string lat{ get; set; }
        public string lon{ get; set; }

        

    }
}
