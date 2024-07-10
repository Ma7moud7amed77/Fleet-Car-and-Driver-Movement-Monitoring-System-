/*using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestGP.DTO;
using TestGP.Models;

namespace TestGP.Controllers
{
    [EnableCors("AllowOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class applicantCertifcatesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public applicantCertifcatesController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> getAllCertifcates()
        {
            var all = await _context.ApplicantCertificates.ToListAsync();
            return Ok(all);
        }
        [HttpPost]
        public async Task<IActionResult> postCertificate(ApplicantCertificate DTO)
        {
            var appCertificate = new ApplicantCertificate
            {
                AppID = DTO.AppID,
                Certificate = DTO.Certificate,
            };
            await _context.AddAsync(appCertificate);
            _context.SaveChanges();
            return Ok(appCertificate);
        }

        [HttpPut("{AppID}/{Certificate}")]
        public async Task<IActionResult> update(int AppID,string Certificate, [FromBody] ApplicantCertificate DTO)
        {
            var applicantCertificate = await _context.ApplicantCertificates.SingleOrDefaultAsync(g => g.AppID == AppID&&g.Certificate==Certificate);
            if (applicantCertificate == null)
                return NotFound($" no Appliacnt was found with ID: {AppID} ");
            applicantCertificate.AppID = AppID;
            applicantCertificate.Certificate = Certificate;
            _context.SaveChanges();
            return Ok(applicantCertificate);

        }

        [HttpDelete]
        public async Task<IActionResult> delete(int AppID, string Certificate)
        {
            var applicantCertificate = await _context.ApplicantCertificates.SingleOrDefaultAsync(g => g.AppID == AppID && g.Certificate == Certificate);
            if (applicantCertificate == null)
                return NotFound($" no Appliacnt was found with ID: {AppID} ");
            _context.Remove(applicantCertificate);
            _context.SaveChanges();
            return Ok("Deleted Successfully :)");

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> getcertificatesByAppId(int id)
        {
            var certificates = await _context.ApplicantCertificates.Where(g=>g.AppID == id).ToListAsync();
            if (certificates == null)
                return NotFound($" no certificates for applicant ID: {id} ");
            return Ok(certificates);
        }
    }
}
*/