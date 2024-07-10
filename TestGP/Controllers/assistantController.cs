using DocumentFormat.OpenXml.Bibliography;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TestGP.DTO;
using TestGP.Models;

namespace TestGP.Controllers
{
    [EnableCors("AllowOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class assistantController : ControllerBase
    {
      //  private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AppDbContext _context;

        public assistantController(AppDbContext context)//, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            //_httpContextAccessor = httpContextAccessor;
        }

        [HttpGet("count")]
        public async Task<IActionResult> CountAll(string _userRole, int _userId)
        {
            var UserRole = _userRole;
            var userId = _userId;

            if (UserRole == null)
            {
                return NotFound("Role data not found.");
            }
            else
            {
                if (UserRole == "SuperAdmin")
                {
                    var count = await _context.Assistants.CountAsync();
                    return Ok(count);
                }
                else if (UserRole == "Manager")
                {
                    var count = await _context.Assistants.Where(a => a.MgrID == userId).CountAsync();
                    return Ok(count);
                }
                else if (UserRole == "Admin")
                {
                    var count = await _context.Assistants.Where(a => a.AdminID == userId).CountAsync();
                    return Ok(count);
                }
                else if (UserRole == "Assistant")
                {
                    var all = await _context.Assistants.Where(a => a.Id == userId).SingleOrDefaultAsync();
                    return Ok(all);

                }
                else
                {
                    return NotFound();
                }
            }
        }

        [HttpGet]
        public async Task<IActionResult> getAllAssistants(string _userRole, int _userId)
        {
            var UserRole = _userRole;
            var userId = _userId;

            if (UserRole == null)
            {
                return NotFound("Role data not found.");
            }
            else
            {
                if (UserRole == "SuperAdmin")
                {
                    var all = await _context.Assistants.ToListAsync();
                    return Ok(all);
                }
                else if (UserRole == "Manager")
                {
                    var all = await _context.Assistants.Where(a => a.MgrID == userId).ToListAsync();
                    return Ok(all);
                }

                else
                {
                    return NotFound();
                }
               }
        }

        [HttpPost]
        public async Task<IActionResult> postAssistant(string _userRole, int _userId, [FromForm] postAssistantdto dto)
        {
            var UserRole = _userRole;
            var userId = _userId;

            if (UserRole == null)
            {
                return NotFound("Role data not found.");
            }
            else
            {
                if (UserRole == "Manager")
                {
                     
                     
                    var Assistant = new Assistant
                    {
                        Name = dto.Name,
                        Phone = dto.Phone,
                        MgrID = userId,
                        Email = dto.Email,
                        Password = dto.Password,
                        Address = dto.Address,
                        Salary = dto.Salary,
                        CompID = dto.CompID,
                        AdminID = dto.AdminID,
                        Img = await DocumentSettings.UploadFileAsync(dto.Img, "Assistants"),
                };
                    await _context.AddAsync(Assistant);
                    _context.SaveChanges();
                    return Ok(Assistant);
                }
                else if (UserRole == "Admin")
                {
                    var admin = await _context.Admins.FindAsync(userId);
                    var Assistant = new Assistant
                    {
                        Name = dto.Name,
                        Phone = dto.Phone,
                        MgrID = admin.MgrID,
                        Email = dto.Email,
                        Password = dto.Password,
                        Address = dto.Address,
                        Salary = dto.Salary,
                        CompID = admin.CompID,
                        AdminID = userId,
                        Img = await DocumentSettings.UploadFileAsync(dto.Img, "Assistants"),
                    };
                    await _context.AddAsync(Assistant);
                    _context.SaveChanges();
                    return Ok(Assistant);
                }
                else
                {
                    return NotFound();
                }
            }
        }

        [HttpPut]
        public async Task<IActionResult> update( [FromForm] postAssistantdto dto, string _userRole, int _userId)
        {
            var UserRole = _userRole;
            var userId = _userId;

            if (UserRole == null)
            {
                return NotFound("Role data not found.");
            }
            else
            {
                if (UserRole == "Assistant")
                {
                    var Assistant = await _context.Assistants.SingleOrDefaultAsync(g => g.Id == userId);
                    if (Assistant == null)
                        return NotFound($"No Assistant was found with ID: {userId}");
                     
                     
                    Assistant.Name = dto.Name;
                    Assistant.Phone = dto.Phone;
                    Assistant.Email = dto.Email;
                    Assistant.Password = dto.Password;
                    Assistant.Address = dto.Address;
                    Assistant.Img = await DocumentSettings.EditFile(dto.Img,Assistant.Img, "Assistants");
                    _context.SaveChanges();
                    return Ok(Assistant);
                }
                else
                {
                    return NotFound();
                }
            }
        }
        [HttpPut("ByAdmin")]
        public async Task<IActionResult> update(int id, [FromForm] postAssistantdto dto, string _userRole, int _userId)
        {
            var UserRole = _userRole;
            var userId = _userId;

            if (UserRole == null)
            {
                return NotFound("Role data not found.");
            }
            else
            {
                 if (UserRole == "Admin")
                {
                    var Assistant = await _context.Assistants.SingleOrDefaultAsync(g => g.Id == id && g.AdminID == userId);
                    if (Assistant == null)
                        return NotFound($"No Assistant was found with ID: {id}");
                     
                     
                    Assistant.Name = dto.Name;
                    Assistant.Phone = dto.Phone;
                    Assistant.Email = dto.Email;
                    Assistant.Password = dto.Password;
                    Assistant.Address = dto.Address;
                    Assistant.Salary = dto.Salary;
                    Assistant.Img = await DocumentSettings.EditFile(dto.Img, Assistant.Img, "Assistants");
                    _context.SaveChanges();
                    return Ok(Assistant);
                }


                else
                {
                    return NotFound();
                }
            }
        }
        [HttpPut("byMgr")]
        public async Task<IActionResult> updateBymanager(int id, [FromForm] postAssistantdto dto, string _userRole, int _userId)
        {
            var UserRole = _userRole;
            var userId = _userId;

            if (UserRole == null)
            {
                return NotFound("Role data not found.");
            }
            else
            {
                if (UserRole == "Manager")
                {
                    var Assistant = await _context.Assistants.SingleOrDefaultAsync(g => g.Id == id && g.MgrID == userId);
                    if (Assistant == null)
                        return NotFound($"No Assistant was found with ID: {id}");
                     
                     
                    Assistant.Name = dto.Name;
                    Assistant.Phone = dto.Phone;
                    Assistant.Email = dto.Email;
                    Assistant.Password = dto.Password;
                    Assistant.Address = dto.Address;
                    Assistant.Salary = dto.Salary;
                    Assistant.CompID = dto.CompID;
                    Assistant.AdminID = dto.AdminID;
                    Assistant.Img = await DocumentSettings.EditFile(dto.Img, Assistant.Img, "Assistants");
                    _context.SaveChanges();
                    return Ok(Assistant);
                }
                
               
                else
                {
                    return NotFound();
                }
            }
        }

        [HttpDelete]
        public async Task<IActionResult> delete(int id, string _userRole, int _userId)
        {
            var UserRole = _userRole;
            var userId = _userId;

            if (UserRole == null)
            {
                return NotFound("Role data not found.");
            }
            else
            {
                if (UserRole == "Manager" || UserRole == "Admin")
                {
                    var Assistant = await _context.Assistants.SingleOrDefaultAsync(g => g.Id == id && (g.MgrID == userId || g.AdminID == userId));
                    if (Assistant == null)
                        return NotFound($"No Assistant was found with ID: {id}");
                    DocumentSettings.DeleteFile(Assistant.Img, "Assistant");
                    _context.Remove(Assistant);
                    _context.SaveChanges();
                    return Ok("Deleted Successfully :)");
                }
                else
                {
                    return NotFound();
                }
            }
        }

        [HttpGet("getAssistantById")]
        public async Task<IActionResult> getAssistantById(int id, string _userRole, int _userId)
        {
            var UserRole = _userRole;
            var userId = _userId;

            if (UserRole == null)
            {
                return NotFound("Role data not found.");
            }
            else
            {
                if (UserRole == "Manager" || UserRole == "Admin")
                {
                    var Assistant = await _context.Assistants.SingleOrDefaultAsync(g => g.Id == id && (g.MgrID == userId || g.AdminID == userId));
                    if (Assistant == null)
                        return NotFound($"No Assistant was found with ID: {id}");
                    return Ok(Assistant);   
                }
                else
                {
                    return NotFound();
                }
            }
        }

        [HttpGet]
        [Route("byMangerId")]
        public async Task<IActionResult> getByMangerId(int id, string _userRole, int _userId)
        {
            var UserRole = _userRole;
            var userId = _userId;

            if (UserRole == null)
            {
                return NotFound("Role data not found.");
            }
            else
            {
                if (UserRole == "Manager" || UserRole == "Admin")
                {
                    var all = await _context.Assistants.Where(g => g.MgrID == id && (g.MgrID == userId || g.AdminID == userId)).ToListAsync();
                    return Ok(all);
                }
                else
                {
                    return NotFound();
                }
            }
        }

        [HttpGet]
        [Route("byCompanyId")]
        public async Task<IActionResult> getByCompanyId(int id, string _userRole, int _userId)
        {
            var UserRole = _userRole;
            var userId = _userId;

            if (UserRole == null)
            {
                return NotFound("Role data not found.");
            }
            else
            {
                if (UserRole == "Manager" || UserRole == "Admin")
                {
                    var all = await _context.Assistants.Where(g => g.CompID == id && (g.MgrID == userId || g.AdminID == userId)).ToListAsync();
                    return Ok(all);
                }
                else
                {
                    return NotFound();
                }
            }
        }

        [HttpGet]
        [Route("byAdminId")]
        public async Task<IActionResult> getByAdminId(int id, string _userRole, int _userId)
        {
            var UserRole = _userRole;
            var userId = _userId;

            if (UserRole == null)
            {
                return NotFound("Role data not found.");
            }
            else
            {
                if (UserRole == "Manager" || UserRole == "Admin")
                {
                    var all = await _context.Assistants.Where(m => m.AdminID == id && (m.MgrID == userId || m.AdminID == userId)).ToListAsync();
                    return Ok(all);
                }
                else
                {
                    return NotFound();
                }
            }
        }
        public IActionResult GetImageURL(string imageName)
        {
            if (imageName != null)
            {
                string imageUrl = DocumentSettings.GetImageUrl(imageName, "Assistants"); // Assuming you have a function to get the image URL
                return Ok(imageUrl);
            }
            return Ok("NotFound");
        }
    }
}
