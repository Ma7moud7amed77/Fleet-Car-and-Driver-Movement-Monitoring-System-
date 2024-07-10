using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Numerics;
using System;
using TestGP.DTO;
using TestGP.Models;
using Microsoft.AspNetCore.Cors;
using ImageHandling.Helper;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Office2010.Excel;

namespace TestGP.Controllers
{
    [EnableCors("AllowOrigin")]
    [Route("api/[controller]")]
    [ApiController]

    public class adminController : ControllerBase
    {
     //   private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AppDbContext _context;
        public adminController(AppDbContext context)//, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
           // _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet("test")]
        public async Task<IActionResult> test()
        {
            var all = await _context.Admins.ToListAsync();
            return Ok(all);
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
                    var count = await _context.Admins.CountAsync();
                    return Ok(count);
                }
                else if (UserRole == "Manager")
                {
                    var count = await _context.Admins.Where(a => a.MgrID == userId).CountAsync();
                    return Ok(count);
                }
               
                else
                {
                    return NotFound();
                }
            }
        }
        [HttpGet]
        public async Task<IActionResult> getAllAdmins(string _userRole, int _userId)
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
                    var all = await _context.Admins.ToListAsync();
                    return Ok(all);
                }
                else if (UserRole=="Manager")
                {
                    var all = await _context.Admins.Where(a=>a.MgrID==userId).ToListAsync();
                    return Ok(all);

                } 
                else if (UserRole=="Admin")
                {
                    var all = await _context.Admins.Where(a=>a.Id==userId).SingleOrDefaultAsync();
                    return Ok(all);

                }

                else
                {
                    return NotFound();
                }
            }
        }

        [HttpPost]
        public async Task<IActionResult> postAdmin(string _userRole, int _userId, [FromForm] postAdmindto dto)
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
                     
                     
                    var Admin = new Admin
                    {

                        Name = dto.Name,
                        Phone = dto.Phone,
                        MgrID =userId ,
                        Email = dto.Email,
                        Password = dto.Password,
                        Address = dto.Address,
                        Salary = dto.Salary,    
                        CompID = dto.CompID,
                        Img = await DocumentSettings.UploadFileAsync(dto.Img, "Admins"),

                    };
                    
                    await _context.AddAsync(Admin);
                    _context.SaveChanges();
                    return Ok(Admin);
                }
                else
                {
                    return NotFound();
                }
            }
        }

        [HttpPut]
        public async Task<IActionResult> update([FromForm] postAdmindto dto, string _userRole, int _userId)
        {
            var UserRole = _userRole;
            var userId = _userId;


            if (UserRole == null)
            {
                return NotFound("Session data not found.");
            }
            else
            {
                  if (UserRole == "Admin")
                {
                    var Admin = await _context.Admins.SingleOrDefaultAsync(g => g.Id == userId);
                     
                     
                    Admin.Name = dto.Name;
                    Admin.Phone = dto.Phone;
                    Admin.Email = dto.Email;
                    Admin.Password = dto.Password;
                    Admin.Address = dto.Address;
                    Admin.Img = await DocumentSettings.EditFile(dto.Img, Admin.Img, "Admins");
                    _context.SaveChanges();
                    return Ok(Admin);
                }
                else
                {
                    return NotFound();
                }
            }
        }


       [HttpPut("ByMgr")]
        public async Task<IActionResult> update(int id, [FromForm] postAdmindto dto, string _userRole, int _userId)
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
                    var Admin = await _context.Admins.SingleOrDefaultAsync(g => g.Id == id&&g.MgrID==userId);
                    if (Admin == null)
                        return NotFound($" no Admin was found with ID: {id} ");
                    Admin.Name = dto.Name;
                    Admin.Phone = dto.Phone;
                    Admin.Email = dto.Email;
                    Admin.Password = dto.Password;
                    Admin.Address = dto.Address;
                    Admin.Salary = dto.Salary;
                    Admin.CompID = dto.CompID;
                    Admin.Img = await DocumentSettings.EditFile(dto.Img, Admin.Img, "Admins"); ;
                    _context.SaveChanges();
                    return Ok(Admin);
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
                if (UserRole == "Manager")
                {
                    var Admin = await _context.Admins.SingleOrDefaultAsync(g => g.Id == id&&g.MgrID==userId);   
                    if (Admin == null)
                        return NotFound($" no Admin was found with ID: {id} ");
                    DocumentSettings.DeleteFile(Admin.Img, "Admins");
                    _context.Remove(Admin);
                    _context.SaveChanges();
                    return Ok("Deleted Successfully :)");
                }
                else
                {
                    return NotFound();
                }
            }
        }

        [HttpGet("getAdminById")]
        public async Task<IActionResult> getAdminById(int id, string _userRole, int _userId)
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
                    var Admin = await _context.Admins.SingleOrDefaultAsync(g => g.Id == id&&g.MgrID==userId);
                    if (Admin == null)
                        return NotFound($" no Admin was found with ID: {id} ");
                    return Ok(Admin);
                }
                else
                {
                    return NotFound();
                }
            }
        }

        [HttpGet]
        [Route("byMangerId")]
        public async Task<IActionResult> getByMangerId(string _userRole, int _userId)//query parameters
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
                    var all = await _context.Admins.Where(m => m.MgrID == userId).ToListAsync();
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
                return NotFound("Session data not found.");
            }
            else
            {
                if (UserRole == "Manager")
                {
                    var all = await _context.Admins.Where(m => m.CompID == id&&m.MgrID==userId).ToListAsync();
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
                string imageUrl = DocumentSettings.GetImageUrl(imageName, "Admins"); // Assuming you have a function to get the image URL
                return Ok(imageUrl);
            }
            return Ok("NotFound");
        }
    }
}
