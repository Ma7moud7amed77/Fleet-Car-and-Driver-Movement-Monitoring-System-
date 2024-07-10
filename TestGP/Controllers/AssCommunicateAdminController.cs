using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestGP.DTO;
using TestGP.Models;

namespace TestGP.Controllers
{
    [EnableCors("AllowOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AssCommunicateAdminController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AppDbContext _context;

        public AssCommunicateAdminController(AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public async Task<IActionResult> getAllcomments()
        {
            var UserRole = _httpContextAccessor.HttpContext.Session.GetString("UserRole");

            if (UserRole == null)
            {
                return NotFound("Session data not found.");
            }
            else
            {
                if (UserRole == "SuperAdmin")
                {
                         var all = await _context.AssCommunicateAdmins.ToListAsync();
                    return Ok(all);
                }
                else
                {
                    return NotFound();
                }
            }
            
        }
        [HttpPost]
        public async Task<IActionResult> postComment(postAssCommunicateAdmindto DTO)
        {
            var AssCommunicateAdmin = new AssCommunicateAdmin
            {
                AssID = DTO.AssID,
                AdminID= DTO.AdminID,
                DateOfMessageAdmin = DTO.DateOfMessageAdmin,
                DateOfMessageAss=DTO.DateOfMessageAss,
                AdminComment=DTO.AdminComment,
                AssComment=DTO.AssComment
    };
            await _context.AddAsync(AssCommunicateAdmin);
            _context.SaveChanges();
            return Ok(AssCommunicateAdmin);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> update(int id, [FromBody] postAssCommunicateAdmindto DTO)
        {
            var assCommunicateAdmin = await _context.AssCommunicateAdmins.SingleOrDefaultAsync(g => g.Id == id);
            if (assCommunicateAdmin == null)
                return NotFound($" no comments with ID: {id} ");
            
            assCommunicateAdmin.AssID = DTO.AssID;
            assCommunicateAdmin.AdminID = DTO.AdminID;
            assCommunicateAdmin.DateOfMessageAss = DTO.DateOfMessageAss;
            assCommunicateAdmin.DateOfMessageAdmin = DTO.DateOfMessageAdmin;
            assCommunicateAdmin.AdminComment = DTO.AdminComment;
            assCommunicateAdmin.AssComment = DTO.AssComment;
            _context.SaveChanges();
            return Ok(assCommunicateAdmin);

        }

        [HttpDelete]
        public async Task<IActionResult> delete(int id)
        {
            var assCommunicateAdmin = await _context.AssCommunicateAdmins.SingleOrDefaultAsync(g => g.Id == id);
            if (assCommunicateAdmin == null)
                return NotFound($" no comments with ID: {id} ");
            _context.Remove(assCommunicateAdmin);
            _context.SaveChanges();
            return Ok("Deleted Successfully :)");

        }
        [HttpGet("allCommentAss/{id}")]
        public async Task<IActionResult> getCommentsByAssId(int id)
        {
            var assCommunicateAdmin = await _context.AssCommunicateAdmins.Where(g => g.AssID == id).ToListAsync();
            if (assCommunicateAdmin == null)
                return NotFound($" no comments with ID: {id} ");
            return Ok(assCommunicateAdmin);
        }
        [HttpGet("allCommentOfAdmin/{id}")]
        public async Task<IActionResult> getCommentsByAdminId(int id)
        {
            var assCommunicateAdmin = await _context.AssCommunicateAdmins.Where(g => g.AdminID == id).ToListAsync();
            if (assCommunicateAdmin == null)
                return NotFound($" no comments with ID: {id} ");
            return Ok(assCommunicateAdmin);
        }
    }
}

