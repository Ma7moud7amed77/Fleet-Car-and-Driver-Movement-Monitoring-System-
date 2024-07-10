/*using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using TestGP.DTO;
using TestGP.Models;

namespace TestGP.Controllers
{
    [EnableCors("AllowOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class applicantController : ControllerBase
    {
        private readonly AppDbContext _context;

        public applicantController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> getAllapplicants()
        {
            var all = await _context.Applicants.ToListAsync();
            return Ok(all);
        }
        [HttpGet("{CompId}")]
        public async Task<IActionResult> getAllapplicantsbyComId(int CompId)
        {
            var all = await _context.Applicants.Where(a=>a.compId==CompId).ToListAsync();
          
            return Ok(all);
        }
        [HttpPost]
        public async Task<IActionResult> postapplicant([FromForm] postApplicantdto dto, int compID)
        {
            try
            {
                // Find the Apply relationship for the specified company ID
                var apply = await _context.Applicants.FindAsync(compID);

                if (apply == null)
                {
                    return NotFound($"Apply relationship for company ID {compID} not found.");
                }

                // Copy the image to a memory stream
                 
                 

                // Create the new applicant
                var applicant = new Applicant
                {
                    Name = dto.Name,
                    Phone = dto.Phone,
                    Email = dto.Email,
                    Password = dto.Password,
                    Address = dto.Address,
                    License = dto.License,
                    LicenseExpDate = dto.LicenseExpDate,
                    Position = dto.Position,
                    Age = dto.Age,
                    //Img = stream.ToArray(),
                    compId = compID,
                };

                // Add the applicant to the context and save changes
                await _context.AddAsync(applicant);
                await _context.SaveChangesAsync();

                return Ok(applicant);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error posting applicant: " + ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> update(int id, [FromForm] postApplicantdto dto)
        {
             
             
            var applicant = await _context.Applicants.SingleOrDefaultAsync(g => g.Id == id);
            if (applicant == null)
                return NotFound($" no applicant was found with ID: {id} ");
            applicant.Name = dto.Name;
            applicant.Phone = dto.Phone;
            applicant.Email = dto.Email;
            applicant.Password = dto.Password;
            applicant.Address = dto.Address;
            applicant.License = dto.License;
            applicant.LicenseExpDate = dto.LicenseExpDate;
            applicant.Age = dto.Age;
            applicant.Position = dto.Position;
           // applicant.Img = stream.ToArray();
            _context.SaveChanges();
            return Ok(applicant);

        }

        [HttpDelete]
        public async Task<IActionResult> delete(int id)
        {
            var applicant = await _context.Applicants.SingleOrDefaultAsync(g => g.Id == id);
            if (applicant == null)
                return NotFound($" no applicant was found with ID: {id} ");
            _context.Remove(applicant);
            _context.SaveChanges();
            return Ok("Deleted Successfully :)");

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> getapplicantById(int id)
        {
            var applicant = await _context.Applicants.SingleOrDefaultAsync(g => g.Id == id);
            if (applicant == null)
                return NotFound($" no applicant was found with ID: {id} ");
            return Ok(applicant);
        }
        [HttpPut("changeState/{id}")]
        public async Task<IActionResult> changeState(int id)
        {
            var applicant = await _context.Applicants.SingleOrDefaultAsync(g => g.Id == id);
            if (applicant == null)
                return NotFound($" no applicant was found with ID: {id} ");
            applicant.State = true;
            _context.SaveChanges();
            return Ok(applicant);
        }
        [HttpPost("AddTocompany/{id}")]
        // open session
        public async Task<IActionResult> addToCompany(int id)
        {
            var applicant = await _context.Applicants.SingleOrDefaultAsync(g => g.Id == id);
            if (applicant == null)
                return NotFound($" no applicant was found with ID: {id} ");
            else
            {
                if (applicant.Position == "Admin")
                {
                    // Create a new Admin based on the applicant
                    var Admin = new Admin
                    {
                        Name = applicant.Name,
                        Phone = applicant.Phone,
                        MgrID = null,
                        Email = applicant.Email,
                        Password = applicant.Password,
                        Address = applicant.Address,
                        Salary = 0,
                        CompID = null

                    };

                    // Add the admin to the admins table
                    _context.Admins.Add(Admin);

                    // Remove the applicant from the applicants table
                    _context.Applicants.Remove(applicant);

                    await _context.SaveChangesAsync();
                    return Ok("done");
                }
                else if (applicant.Position == "Assistant")
                {
                    // Create a new Assistant based on the applicant
                    var Assistant = new Assistant
                    {
                        Name = applicant.Name,
                        Phone = applicant.Phone,
                        MgrID = null,
                        Email = applicant.Email,
                        Password = applicant.Password,
                        Address = applicant.Address,
                        Salary = 0,
                        CompID = null,
                        AdminID = null

                    };

                    // Add the assistant to the assistants table
                    _context.Assistants.Add(Assistant);

                    // Remove the applicant from the applicants table
                    _context.Applicants.Remove(applicant);

                    await _context.SaveChangesAsync();
                    return Ok("done");
                }
                else
                {
                    // Create a new Driver based on the applicant
                    var Driver = new Driver
                    {
                        Name = applicant.Name,
                        Phone = applicant.Phone,
                        MgrID = null,
                        Email = applicant.Email,
                        Password = applicant.Password,
                        Address = applicant.Address,
                        Salary = 0,
                        License=applicant.License,
                        LicenseExpDate= applicant.LicenseExpDate,
                        CompID = null,
                        AdminID = null,
                        AssID = null

                    };

                    // Add the driver to the drivers table
                    _context.Drivers.Add(Driver);

                    // Remove the applicant from the applicants table
                    _context.Applicants.Remove(applicant);

                    await _context.SaveChangesAsync();
                    return Ok("done");
                }
            }

        }
        [HttpGet("getImage")]
        public IActionResult GetImageURL(string imageName)
        {
            if (imageName != null)
            {
                string imageUrl = DocumentSettings.GetImageUrl(imageName, "Applicants"); // Assuming you have a function to get the image URL
                return Ok(imageUrl);
            }
            return Ok("NotFound");
        }
    }
}
*/