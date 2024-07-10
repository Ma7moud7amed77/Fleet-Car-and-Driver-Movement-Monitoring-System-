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
    public class driverCommunicateAssController : ControllerBase
    {
        private readonly AppDbContext _context;

        public driverCommunicateAssController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> getAllcomments()
        {
            var all = await _context.DriverCommunicateAsses.ToListAsync();
            return Ok(all);
        }
        [HttpPost]
        public async Task<IActionResult> postComment(postDriverCommunicateAssdto DTO)
        {
            var DriverCommunicateAss = new DriverCommunicateAss
            {
                
                AssID = DTO.AssID,
                DriverID = DTO.DriverID,
                DateOfMessageAss = DTO.DateOfMessageAss,
                DateOfMessageDriver = DTO.DateOfMessageDriver,
                DriverComment = DTO.DriverComment,
                AssComment = DTO.AssComment
            };
            await _context.AddAsync(DriverCommunicateAss);
            _context.SaveChanges();
            return Ok(DriverCommunicateAss);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> update(int id, [FromBody] postDriverCommunicateAssdto DTO)
        {
            var DriverCommunicateAss = await _context.DriverCommunicateAsses.SingleOrDefaultAsync(g => g.Id == id);
            if (DriverCommunicateAss == null)
                return NotFound($" no comments with ID: {id} ");
            
            DriverCommunicateAss.AssID = DTO.AssID;
            DriverCommunicateAss.DriverID = DTO.DriverID;
            DriverCommunicateAss.DateOfMessageAss = DTO.DateOfMessageAss;
            DriverCommunicateAss.DateOfMessageDriver = DTO.DateOfMessageDriver;
            DriverCommunicateAss.DriverComment = DTO.DriverComment;
            DriverCommunicateAss.AssComment = DTO.AssComment;
            _context.SaveChanges();
            return Ok(DriverCommunicateAss);

        }

        [HttpDelete]
        public async Task<IActionResult> delete(int id)
        {
            var DriverCommunicateAss = await _context.DriverCommunicateAsses.SingleOrDefaultAsync(g => g.Id == id);
            if (DriverCommunicateAss == null)
                return NotFound($" no comments with ID: {id} ");
            _context.Remove(DriverCommunicateAss);
            _context.SaveChanges();
            return Ok("Deleted Successfully :)");

        }
        [HttpGet("allCommentOfDriver/{id}")]
        public async Task<IActionResult> allCommentByDriverId(int id)
        {
            var DriverCommunicateAss = await _context.DriverCommunicateAsses.Where(g => g.DriverID == id).ToListAsync();
            if (DriverCommunicateAss == null)
                return NotFound($" no comments with ID: {id} ");
            return Ok(DriverCommunicateAss);
        }
        [HttpGet("allCommentOfAss/{id}")]
        public async Task<IActionResult> allCommentByAssId(int id)
        {
            var DriverCommunicateAss = await _context.DriverCommunicateAsses.Where(g => g.AssID == id).ToListAsync();
            if (DriverCommunicateAss == null)
                return NotFound($" no comments with ID: {id} ");
            return Ok(DriverCommunicateAss);
        }
    }
}
