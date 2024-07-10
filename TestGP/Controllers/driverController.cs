using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TestGP.DTO;
using TestGP.Models;

namespace TestGP.Controllers
{
    [EnableCors("AllowOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class driverController : ControllerBase
    {
        private readonly AppDbContext _context;

        public driverController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("count")]
        public async Task<IActionResult> CountAll(string _userRole, int _userId)
        {
            var UserRole = _userRole;
            var userId = _userId;

            if (UserRole == null)
            {
                return NotFound("User role not provided.");
            }
            else
            {
                int count;
                if (UserRole == "SuperAdmin")
                {
                    count = await _context.Drivers.CountAsync();
                }
                else if (UserRole == "Manager")
                {
                    count = await _context.Drivers.Where(a => a.MgrID == userId).CountAsync();
                }
                else if (UserRole == "Admin")
                {
                    count = await _context.Drivers.Where(a => a.AdminID == userId).CountAsync();
                }
                else if (UserRole == "Assistant")
                {
                    count = await _context.Drivers.Where(a => a.AssID == userId).CountAsync();
                }
                else
                {
                    return NotFound();
                }
                return Ok(count);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDrivers(string _userRole, int _userId)
        {
            var UserRole = _userRole;
            var userId = _userId;

            if (UserRole == null)
            {
                return NotFound("User role not provided.");
            }
            else
            {
                if (UserRole == "SuperAdmin")
                {
                    var all = await _context.Drivers.ToListAsync();
                    return Ok(all);
                }
                else if (UserRole == "Manager")
                {
                    var all = await _context.Drivers.Where(a => a.MgrID == userId).ToListAsync();
                    return Ok(all);

                }
                else if (UserRole == "Admin")
                {
                    var all = await _context.Drivers.Where(a => a.Id == userId).ToListAsync();
                    return Ok(all);

                }
                else if (UserRole == "Assistant")
                {
                    var all = await _context.Drivers.Where(a => a.AssID == userId).ToListAsync();
                    return Ok(all);

                }
                else if (UserRole == "Driver")
                {
                    var all = await _context.Drivers.Where(a => a.Id == userId).SingleOrDefaultAsync();
                    return Ok(all);

                }

                else
                {
                    return NotFound();
                }
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostDriver([FromForm] postDriverdto dto, string _userRole, int _userId)
        {
            var UserRole = _userRole;
            var userId = _userId;

            if (UserRole == null)
            {
                return NotFound("User role not provided.");
            }
            else
            {
                if (UserRole == "Manager")
                {
                     
                     
                    var driver = new Driver
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
                        AssID = dto.AssID,
                        License = dto.License,
                        LicenseExpDate = dto.LicenseExpDate,
                        Health_statusd = dto.Health_statusd,
                        Img = await DocumentSettings.UploadFileAsync(dto.Img, "Drivers"),
                    };
                    await _context.AddAsync(driver);
                    await _context.SaveChangesAsync();
                    return Ok(driver);
                }
                else if (UserRole == "Admin")
                {
                    var admin = await _context.Admins.FirstOrDefaultAsync(a => a.Id == userId);
                     
                     
                    var driver = new Driver
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
                        AssID = dto.AssID,
                        License = dto.License,
                        LicenseExpDate = dto.LicenseExpDate,
                        Health_statusd = dto.Health_statusd,
                        Img = await DocumentSettings.UploadFileAsync(dto.Img, "Drivers"),
                    };
                    await _context.AddAsync(driver);
                    await _context.SaveChangesAsync();
                    return Ok(driver);
                }
                else
                {
                    return NotFound();
                }
            }
        }

        [HttpPut("bydriver")]
        public async Task<IActionResult> Update([FromForm] postDriverdto dto, string _userRole, int _userId)
        {
            var UserRole = _userRole;
            var userId = _userId;

            if (UserRole == null)
            {
                return NotFound("User role not provided.");
            }
            else
            {
                
                 if (UserRole == "Driver")
                {
                     
                     
                    var driver = await _context.Drivers.SingleOrDefaultAsync(d => d.Id == userId);
                    if (driver == null)
                        return NotFound($"No driver was found with ID: {userId}");
                    driver.Name = dto.Name;
                    driver.Phone = dto.Phone;
                    driver.Email = dto.Email;
                    driver.Password = dto.Password;
                    driver.Address = dto.Address;   
                    driver.License=driver.License;
                    driver.LicenseExpDate = driver.LicenseExpDate;
                   driver.Img = await DocumentSettings.EditFile(dto.Img, driver.Img, "Drivers");
                    await _context.SaveChangesAsync();
                    return Ok(driver);
                }
                else
                {
                    return NotFound();
                }
            }
        } 
        [HttpPut("byMgr")]
        public async Task<IActionResult> Update(int id, [FromForm] postDriverdto dto, string _userRole, int _userId)
        {
            var UserRole = _userRole;
            var userId = _userId;

            if (UserRole == null)
            {
                return NotFound("User role not provided.");
            }
            else
            {
                if (UserRole == "Manager")
                {
                     
                     
                    var driver = await _context.Drivers.SingleOrDefaultAsync(d => d.Id == id && d.MgrID == userId);
                    if (driver == null)
                        return NotFound($"No driver was found with ID: {id}");
                    driver.Name = dto.Name;
                    driver.Phone = dto.Phone;
                    driver.MgrID = userId;
                    driver.Email = dto.Email;
                    driver.Password = dto.Password;
                    driver.Address = dto.Address;
                    driver.Salary = dto.Salary;
                    driver.CompID = dto.CompID;
                    driver.AdminID = dto.AdminID;
                    driver.AssID = dto.AssID;
                    driver.License = dto.License;
                    driver.LicenseExpDate = dto.LicenseExpDate;
                    driver.Health_statusd = dto.Health_statusd;
                    driver.Img = await DocumentSettings.EditFile(dto.Img, driver.Img, "Drivers");
                    await _context.SaveChangesAsync();
                    return Ok(driver);
                }
                else
                {
                    return NotFound();
                }
            }
        }
        
        [HttpPut("byAdmin")]
        public async Task<IActionResult> UpdateByAdmin(int id, [FromForm] postDriverdto dto, string _userRole, int _userId)
        {
            var UserRole = _userRole;
            var userId = _userId;

            if (UserRole == null)
            {
                return NotFound("User role not provided.");
            }
            else
            {
               
                 if (UserRole == "Admin")
                {
                    var admin = await _context.Admins.FirstOrDefaultAsync(a => a.Id == userId);
                     
                     
                    var driver = await _context.Drivers.SingleOrDefaultAsync(d => d.Id == id && d.AdminID == userId);
                    if (driver == null)
                        return NotFound($"No driver was found with ID: {id}");
                    driver.Name = dto.Name;
                    driver.Phone = dto.Phone;
                    driver.Email = dto.Email;
                    driver.Password = dto.Password;
                    driver.Address = dto.Address;
                    driver.Salary = dto.Salary;
                    driver.AdminID = userId;
                    driver.CompID = admin.CompID;
                    driver.AssID = dto.AssID;
                    driver.License = dto.License;
                    driver.LicenseExpDate = dto.LicenseExpDate;
                    driver.Health_statusd = dto.Health_statusd;
                    driver.Img = await DocumentSettings.EditFile(dto.Img, driver.Img, "Drivers");
                    await _context.SaveChangesAsync();
                    return Ok(driver);
                }
                
                else
                {
                    return NotFound();
                }
            }
        }
     
        [HttpDelete]
        public async Task<IActionResult> Delete(int id, string _userRole, int _userId)
        {
            var UserRole = _userRole;
            var userId = _userId;

            if (UserRole == null)
            {
                return NotFound("User role not provided.");
            }
            else
            {
                if (UserRole == "Manager")
                {
                    var driver = await _context.Drivers.SingleOrDefaultAsync(d => d.Id == id && d.MgrID == userId);
                    if (driver == null)
                        return NotFound($"No driver was found with ID: {id}");
                    //DocumentSettings.DeleteFile(driver.Img, "Drivers");
                    _context.Remove(driver);
                    await _context.SaveChangesAsync();
                    return Ok("Deleted Successfully :)");
                }
                else if (UserRole == "Admin")
                {
                    var driver = await _context.Drivers.SingleOrDefaultAsync(d => d.Id == id && d.AdminID == userId);
                    if (driver == null)
                        return NotFound($"No driver was found with ID: {id}");
                    DocumentSettings.DeleteFile(driver.Img, "Drivers");
                    _context.Remove(driver);
                    await _context.SaveChangesAsync();
                    return Ok("Deleted Successfully :)");
                }
                else
                {
                    return NotFound();
                }
            }
        }

        [HttpGet("byId")]
        public async Task<IActionResult> GetDriverById(int id, string _userRole, int _userId)
        {
            var UserRole = _userRole;
            var userId = _userId;

            if (UserRole == null)
            {
                return NotFound("User role not provided.");
            }
            else
            {
                if (UserRole == "Manager")
                {
                    var driver = await _context.Drivers.SingleOrDefaultAsync(d => d.Id == id && d.MgrID == userId);
                    if (driver == null)
                        return NotFound($"No driver was found with ID: {id}");
                    return Ok(driver);
                }
                else if (UserRole == "Admin")
                {
                    var driver = await _context.Drivers.SingleOrDefaultAsync(d => d.Id == id && d.AdminID == userId);
                    if (driver == null)
                        return NotFound($"No driver was found with ID: {id}");
                    return Ok(driver);
                }
                else if (UserRole == "Assistant")
                {
                    var driver = await _context.Drivers.SingleOrDefaultAsync(d => d.Id == id && d.AssID == userId);
                    if (driver == null)
                        return NotFound($"No driver was found with ID: {id}");
                    return Ok(driver);
                }
                else
                {
                    return NotFound();
                }
            }
        }

        [HttpGet]
        [Route("byMgr")]
        public async Task<IActionResult> GetAllDriversByManagerId(string _userRole, int _userId)
        {
            var UserRole = _userRole;
            var userId = _userId;

            if (UserRole == null)
            {
                return NotFound("User role not provided.");
            }
            else
            {
                if (UserRole == "Manager")
                {
                    var all = await _context.Drivers.Where(m => m.MgrID == userId).ToListAsync();
                    return Ok(all);
                }
                else
                {
                    return NotFound();
                }
            }
        }

        [HttpGet]
        [Route("byComp")]
        public async Task<IActionResult> GetAllDriversByCompanyId(int id, string _userRole, int _userId)
        {
            var UserRole = _userRole;
            var userId = _userId;

            if (UserRole == null)
            {
                return NotFound("User role not provided.");
            }
            else
            {
                if (UserRole == "Manager")
                {
                    var all = await _context.Drivers.Where(d => d.CompID == id && d.MgrID == userId).ToListAsync();
                    return Ok(all);
                }
                else if (UserRole == "Admin")
                {
                    var admin = await _context.Admins.FirstOrDefaultAsync(g => g.Id == userId);
                    var all = await _context.Drivers.Where(d => d.CompID == admin.CompID).ToListAsync();
                    return Ok(all);
                }
                else if (UserRole == "Assistant")
                {
                    var assistant = await _context.Assistants.FirstOrDefaultAsync(g => g.Id == userId);
                    var all = await _context.Drivers.Where(d => d.CompID == assistant.CompID).ToListAsync();
                    return Ok(all);
                }
                else
                {
                    return NotFound();
                }
            }
        }

        [HttpGet]
        [Route("byAdmin")]
        public async Task<IActionResult> GetAllDriversByAdminId(int id, string _userRole, int _userId)
        {
            var UserRole = _userRole;
            var userId = _userId;

            if (UserRole == null)
            {
                return NotFound("User role not provided.");
            }
            else
            {
                if (UserRole == "Manager")
                {
                    var all = await _context.Drivers.Where(m => m.AdminID == id && m.MgrID == userId).ToListAsync();
                    return Ok(all);
                }
                else if (UserRole == "Admin")
                {
                    var all = await _context.Drivers.Where(m => m.AdminID == userId).ToListAsync();
                    return Ok(all);
                }
                else if (UserRole == "Assistant")
                {
                    var all = await _context.Drivers.Where(m => m.AdminID == id && m.AssID == userId).ToListAsync();
                    return Ok(all);
                }
                else
                {
                    return NotFound();
                }
            }
        }

        [HttpGet]
        [Route("byAss")]
        public async Task<IActionResult> GetAllDriversByAssistantId(int id, string _userRole, int _userId)
        {
            var UserRole = _userRole;
            var userId = _userId;

            if (UserRole == null)
            {
                return NotFound("User role not provided.");
            }
            else
            {
                if (UserRole == "Manager")
                {
                    var all = await _context.Drivers.Where(m => m.AssID == id && m.MgrID == userId).ToListAsync();
                    return Ok(all);
                }
                else if (UserRole == "Admin")
                {
                    var all = await _context.Drivers.Where(m => m.AssID == id && m.AdminID == userId).ToListAsync();
                    return Ok(all);
                }
                else if (UserRole == "Assistant")
                {
                    var all = await _context.Drivers.Where(m => m.AssID == userId).ToListAsync();
                    return Ok(all);
                }
                else
                {
                    return NotFound();
                }
            }
        }
        [HttpGet("getImage")]
        public IActionResult GetImageURL(string imageName)
        {
            if (imageName != null)
            {
                string imageUrl = DocumentSettings.GetImageUrl(imageName, "Drivers"); // Assuming you have a function to get the image URL
                return Ok(imageUrl);
            }
            return Ok("NotFound");
        }
    }
}
