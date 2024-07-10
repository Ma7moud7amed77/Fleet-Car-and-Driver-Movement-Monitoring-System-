using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TestGP.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Phone { get; set; }
        //one Manager has  many Company
        [ForeignKey("Manager")]
        public int? MgrID { get; set; }
        [JsonIgnore]
        public Manager? Manager { get; set; }
        //Many to Many Relationship with Applicant


        //one Company work with many Driver
        [JsonIgnore]
        public List<Driver>? Drivers { get; set; }
        //one Company work with many Assistat
        [JsonIgnore]
        public List<Assistant>? Assistants { get; set; }
        [JsonIgnore]
        //one Company has  many Cars
        public List<Car>? Cars { get; set; }
       /* [ForeignKey("applicants")]
        public int appId { get; set; }*/
       // public List<Applicant>? applicants { get; set; }



    }
}
