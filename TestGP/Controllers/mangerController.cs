using DocumentFormat.OpenXml.Bibliography;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Net;
using System.Numerics;
using TestGP.DTO;
using TestGP.Models;

namespace TestGP.Controllers
{
    [EnableCors("AllowOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class managerController : ControllerBase
    {
        private readonly AppDbContext _context;
        //private readonly IHttpContextAccessor _httpContextAccessor;

        public managerController(AppDbContext context)//, IHttpContextAccessor httpContextAccessor)
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

                return NotFound("Session data not found.");
            }
            else
            {
                if (UserRole == "SuperAdmin")
                {
                    var count = await _context.Managers.CountAsync();
                    return Ok(count);
                }
                else
                {
                    return NotFound();
                }
            }
        }
        [HttpGet]
        public async Task<IActionResult> getAllmanagers(string _userRole, int _userId)
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
                    var all = await _context.Managers.ToListAsync();
                    return Ok(all);
                }
                else if (UserRole == "Manager")
                {
                    var all = await _context.Managers.Where(a => a.Id == userId).SingleOrDefaultAsync();
                    return Ok(all);

                }
                else
                {
                    return NotFound();
                }
            }
        }
        [HttpPost]
        public async Task<IActionResult> postManger([FromForm]postMangerDto dto, string _userRole, int _userId)
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
                    TimeZoneInfo egyptTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
                    
                    var manger = new Manager
                    {

                        Name = dto.Name,
                        Phone = dto.Phone,
                        Email = dto.Email,
                        Password = dto.Password,
                        Address = dto.Address,
                        Subcribtion = dto.Subcribtion,
                        DateOfSubscribtion = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, egyptTimeZone),
                        SuperAdminId = userId,
                       
 
                        Img= await DocumentSettings.UploadFileAsync(dto.Img, "Managers"),

                    };
                    await _context.AddAsync(manger);
                    _context.SaveChanges();
                    return Ok(manger);
                }
                else
                {
                    return NotFound();
                }
            }
        }
        [HttpPut("byMgrId")]
        public async Task<IActionResult> update(int MgrId, [FromForm] postMangerDto dto, string _userRole, int _userId)
        {
            var UserRole = _userRole;
            var userId = _userId;

            if (UserRole == null)
            {
                return NotFound("Session data not found.");
            }
            else
            {
                var manger = await _context.Managers.SingleOrDefaultAsync(g => g.Id == MgrId);
                if (manger == null)
                    return NotFound($" no manger was found with ID: {MgrId} ");
                if (UserRole == "SuperAdmin")
                {
                    manger.Name = dto.Name;
                    manger.Phone = dto.Phone;
                    manger.Email = dto.Email;
                    manger.Password = dto.Password;
                    manger.Address = dto.Address;
                    manger.Subcribtion = dto.Subcribtion;
                    manger.DateOfSubscribtion = dto.DateOfSubscribtion;
                    manger.SuperAdminId = userId;

                    manger.Img = await DocumentSettings.EditFile(dto.Img, manger.Img, "Managers");
                    _context.SaveChanges();
                    return Ok(manger);
                }
                else
                {
                    return NotFound();
                }

            }
        }
        [HttpPut]
        public async Task<IActionResult> update([FromForm] postMangerDto dto, string _userRole, int _userId)
        {
            var UserRole = _userRole;
            var userId = _userId;

            if (UserRole == null)
            {
                return NotFound("Session data not found.");
            }
            else
            {
                var manger = await _context.Managers.SingleOrDefaultAsync(g => g.Id == userId);
                if (manger == null)
                    return NotFound($" no manger was found with ID: {userId} ");
                if (UserRole == "Manager")
                {
                    manger.Name = dto.Name;
                    manger.Phone = dto.Phone;
                    manger.Email = dto.Email;
                    manger.Password = dto.Password;
                    manger.Address = dto.Address;
                   
                    
                    _context.SaveChanges();
                    return Ok(manger);
                }
                else if (UserRole == "Manager")
                {
                     
                     
                    manger.Name = dto.Name;
                    manger.Phone = dto.Phone;
                    manger.Email = dto.Email;
                    manger.Password = dto.Password;
                    manger.Address = dto.Address;
                    manger.Img= await DocumentSettings.EditFile(dto.Img, manger.Img, "Managers");
                   
                    _context.SaveChanges();
                    return Ok(manger);
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
                if (UserRole == "SuperAdmin")
                {
                    
                    var manger = await _context.Managers.SingleOrDefaultAsync(g => g.Id == id&&g.SuperAdminId==_userId);
                    if (manger == null)
                        return NotFound($" no manger was found with ID: {id} ");
                    DocumentSettings.DeleteFile(manger.Img, "Managers");
                    _context.Remove(manger);
                    _context.SaveChanges();
                    return Ok("Deleted Successfully :)");
                }
                else
                {
                    return NotFound();
                }
            }

        }
        [HttpGet("byMgrId")]
        public async Task<IActionResult> getMangerById(int id, string _userRole, int _userId)
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
                    var manger = await _context.Managers.SingleOrDefaultAsync(g => g.Id == id);
                    if (manger == null)
                        return NotFound($" no manger was found with ID: {id} ");
                    return Ok(manger);
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
                string imageUrl = DocumentSettings.GetImageUrl(imageName, "Managers"); // Assuming you have a function to get the image URL
                return Ok(imageUrl);
            }
            return Ok("NotFound");
        }
    }
}