/*using System.Text.Json.Serialization;

namespace TestGP.Models
{
    [PrimaryKey("AppID", "Certificate")]
    public class ApplicantCertificate
    {
        [ForeignKey("Applicant")]
        public int AppID { get; set; }
        [JsonIgnore]
        public Applicant? Applicant { get; set; }
        public string Certificate { get; set; }
    }
}
*/