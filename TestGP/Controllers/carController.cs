using DocumentFormat.OpenXml.Bibliography;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TestGP.DTO;
using TestGP.Models;

namespace TestGP.Controllers
{
    [EnableCors("AllowOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class carController : ControllerBase
    {
       
        private readonly AppDbContext _context;

        public carController(AppDbContext context)//, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
           // _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet("count")]
        public async Task<IActionResult> CountAll(string _userRole, int _userId)
        {
            var UserRole = _userRole;
            var userId = _userId;

            if (UserRole == null)
            {
                return NotFound("Session data not found.");
            }
            else
            {
                if (UserRole == "SuperAdmin")
                {
                    var count = await _context.Cars.CountAsync();
                    return Ok(count);
                }
                else if (UserRole == "Manager")
                {
                    var count = await _context.Cars.Where(a => a.MgrID == userId).CountAsync();
                    return Ok(count);
                }
                else if (UserRole == "Admin")
                {
                    var count = await _context.Cars.Where(a => a.Assistant.AdminID == userId).CountAsync();
                    return Ok(count);
                }
                else if (UserRole == "Assistant")
                {
                    var count = await _context.Cars.Where(a => a.AssId == userId).CountAsync();
                    return Ok(count);
                }
                else
                {
                    return NotFound();
                }
            }
        }

        [HttpGet]
        public async Task<IActionResult> getAllCars(string _userRole, int _userId)
        {
            var UserRole = _userRole;
            var userId = _userId;
            if (UserRole == null)
            {
                return NotFound("Session data not found.");
            }
            else
            {
                if (UserRole == "SuperAdmin")
                {
                    var all = await _context.Cars.ToListAsync();
                    return Ok(all);
                }
                else
                {
                    return NotFound();
                }
            }
        }

        [HttpPost]
        public async Task<IActionResult> postCar(string _userRole, int _userId, [FromForm] postCardto dto)
        {
            var UserRole = _userRole;
            var userId = _userId;

            if (UserRole == null)
            {
                return NotFound("Session data not found.");
            }
            else
            {
                if (UserRole == "Manager")
                {
                    var Car = new Car
                    {
                        Name = dto.Name,
                        Model = dto.Model,
                        License = dto.License,
                        LicensExpDate = dto.LicensExpDate,
                        CompID = dto.CompID,
                        AdminId = dto.AdminId,
                        AssId = dto.AssId,
                        MgrID = userId,
                        Img = await DocumentSettings.UploadFileAsync(dto.Img, "Cars"),
                    };

                    await _context.AddAsync(Car);
                    await _context.SaveChangesAsync();
                    return Ok(Car);
                }
                else if (UserRole == "Admin")
                {
                    var admin = await _context.Admins.FindAsync(userId);
                    var Car = new Car
                    {
                        Name = dto.Name,
                        Model = dto.Model,
                        License = dto.License,
                        LicensExpDate = dto.LicensExpDate,
                        CompID = admin.CompID,
                        AdminId = userId,
                        AssId = dto.AssId,
                        MgrID = admin.MgrID,
                        Img = await DocumentSettings.UploadFileAsync(dto.Img, "Cars"),
                    };
                    await _context.AddAsync(Car);
                    await _context.SaveChangesAsync();
                    return Ok(Car);
                }
                else
                {
                    return NotFound();
                }
            }
        }

        [HttpPut]
        public async Task<IActionResult> update(int id, [FromForm] postCardto dto, string _userRole, int _userId)
        {
            var UserRole = _userRole;
            var userId = _userId;

            if (UserRole == null)
            {
                return NotFound("Session data not found.");
            }
            else
            {
                if (UserRole == "Manager")
                {
                  //  var admin = await _context.Admins.FindAsync(userId);

                    var Car = await _context.Cars.Include(c=>c.Company).SingleOrDefaultAsync(g => g.Id == id && g.MgrID== userId);
                    if (Car == null)
                        return NotFound($"No Car was found with ID: {id}");
                    Car.Name = dto.Name;
                    Car.License = dto.License;
                    Car.LicensExpDate = dto.LicensExpDate;
                    Car.Model = dto.Model;  
                    Car.CompID = dto.CompID;
                    Car.AdminId = dto.AdminId;
                    Car.AssId = dto.AssId;
                    Car.MgrID = userId;
                    Car.Img= await DocumentSettings.EditFile(dto.Img, Car.Img, "Cars");

                    await _context.SaveChangesAsync();
                    return Ok(Car);
                }
                else if (UserRole == "Admin")
                {
                  //  var admin = await _context.Admins.FindAsync(userId);

                    var Car = await _context.Cars.Include(c=>c.Company).SingleOrDefaultAsync(g => g.Id == id && ( g.AdminId == userId));
                    if (Car == null)
                        return NotFound($"No Car was found with ID: {id}");
                    Car.Name = dto.Name;
                    Car.License = dto.License;
                    Car.LicensExpDate = dto.LicensExpDate;
                    Car.Model = dto.Model;      
                    Car.AdminId = userId;
                    Car.AssId = dto.AssId;
                    Car.Img= await DocumentSettings.EditFile(dto.Img, Car.Img, "Cars");

                    await _context.SaveChangesAsync();
                    return Ok(Car);
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
                return NotFound("Session data not found.");
            }
            else
            {
                if (UserRole == "Manager" || UserRole == "Admin")
                {
                    var Car = await _context.Cars.SingleOrDefaultAsync(g => g.Id == id && (g.MgrID == userId || g.AdminId == userId));
                    if (Car == null)
                        return NotFound($"No Car was found with ID: {id}");
                    DocumentSettings.DeleteFile(Car.Img, "Cars");
                    _context.Remove(Car);
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
        public async Task<IActionResult> getCarById(int id, string _userRole, int _userId)
        {
            var UserRole = _userRole;
            var userId = _userId;
            if (UserRole == null)
            {
                return NotFound("Session data not found.");
            }
            else
            {
                if (UserRole == "Manager" || UserRole == "Admin")
                {
                    var admin = await _context.Admins.FindAsync(userId);
                    var manager = await _context.Admins.FindAsync(userId);
                    var Car = await _context.Cars.FirstOrDefaultAsync(c => c.Id == id && (c.MgrID == userId || c.AdminId == userId));
                    if (Car == null)
                        return NotFound($"No Car was found with ID: {id}");
                    return Ok(Car);
                }
                else
                {
                    return NotFound();
                }
            }
        }

        [HttpGet("byMgrOrAdmin")]
        public async Task<IActionResult> byMgrOrAdmin(string _userRole, int _userId)
        {
            var UserRole = _userRole;
            var userId = _userId;
            if (UserRole == null)
            {
                return NotFound("Session data not found.");
            }
            else
            {
                if (UserRole == "Manager" || UserRole == "Admin")
                {
                    var all = await _context.Cars.Where(c => c.MgrID == userId || c.AdminId == userId).ToListAsync();
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
                string imageUrl = DocumentSettings.GetImageUrl(imageName, "Cars"); // Assuming you have a function to get the image URL
                return Ok(imageUrl);
            }
            return Ok("NotFound");
        }
    }
}
