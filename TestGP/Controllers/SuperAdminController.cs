using DocumentFormat.OpenXml.Bibliography;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Numerics;
using System.Runtime.InteropServices;
using TestGP.DTO;
using TestGP.Models;

namespace TestGP.Controllers
{
    [EnableCors("AllowOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class SuperAdminController : ControllerBase
    {
        // private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AppDbContext _context;

        public SuperAdminController(AppDbContext context)//, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            // _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public async Task<IActionResult> getAllDetailsAboutSupAdminAndSupCompany(string _userRole, int _userId)
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
                    var all = await _context.SuperAdminCompany.ToListAsync();
                    return Ok(all);
                }
                else
                {
                    return NotFound();
                }
            }
        }

        [HttpGet("test")]
        public async Task<IActionResult> test()
        {

            var all = await _context.SuperAdminCompany.ToListAsync();
            return Ok(all);

        }
        [HttpPost]
        public async Task<IActionResult> postSuperAdmin([FromForm] postSuperAdminCompanyDto dto)
        {
            var super = new SuperAdminCompany
            {
                Name = dto.Name,
                Phone = dto.Phone,
                Email = dto.Email,
                Password = dto.Password,
                Address = dto.Address,
                MyComp_id = dto.MyComp_id,
                MyComp_Name = dto.MyComp_Name,
                MyComp_Location = dto.MyComp_Location,
                MyComp_Phone = dto.MyComp_Phone,
                Img = await DocumentSettings.UploadFileAsync(dto.Img, "SuperAdmin"),
            };
            await _context.AddAsync(super);
            _context.SaveChanges();
            return Ok(super);
        }

               
        [HttpPut("{id}")]
        public async Task<IActionResult> update([FromForm] postSuperAdminCompanyDto dto, string _userRole, int _userId)
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

                    var super = await _context.SuperAdminCompany.SingleOrDefaultAsync(g => g.Id == userId);
                    if (super == null)
                        return NotFound($" no super admin was found with ID: {userId} ");
                     
                     
                    super.Name = dto.Name;
                    super.Phone = dto.Phone;
                    super.Email = dto.Email;
                    super.Password = dto.Password;
                    super.Address = dto.Address;
                    super.MyComp_id = dto.MyComp_id;
                    super.MyComp_Name = dto.MyComp_Name;
                    super.MyComp_Location = dto.MyComp_Location;
                    super.MyComp_Phone = dto.MyComp_Phone;
                  super.Img = await DocumentSettings.EditFile(dto.Img, super.Img, "SuperAdmin");

                    _context.SaveChanges();
                    return Ok(super);


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
                    var super = await _context.SuperAdminCompany.SingleOrDefaultAsync(g => g.Id == id);
                    if (super == null)
                        return NotFound($" no super admin was found with ID: {id} ");
                   // DocumentSettings.DeleteFile(super.Img, "SuperAdmin");
                    _context.Remove(super);
                    _context.SaveChanges();
                    return Ok("Deleted Successfully :)");
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
                string imageUrl = DocumentSettings.GetImageUrl(imageName, "SuperAdmin"); // Assuming you have a function to get the image URL
                return Ok(imageUrl);
            }
            return Ok("NotFound");
        }
    }
}

