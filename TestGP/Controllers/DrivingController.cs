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
    
    public class DrivingController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DrivingController(AppDbContext dbContext)
        {
           _context = dbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllDriving()
        {
            var all=await _context.Drivings.ToListAsync();

            return Ok(all);
        } 
        [HttpGet("byCarId")]
        public async Task<IActionResult> getByCar(int id)
        {
            var all=await _context.Drivings.Where(c=>c.CarID==id).ToListAsync();

            return Ok(all);
        }
        [HttpGet("byDriverId")]
        public async Task<IActionResult> getByDriver(int id)
        {
            var all=await _context.Drivings.Where(c=>c.DriverID== id).ToListAsync();

            return Ok(all);
        }
        
        TimeZoneInfo egyptTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
        [HttpPost]
        public async Task<IActionResult> PostDriving([FromBody]postDrivingdto dto)
        {
            try
            {

            var driving = new Driving()
            {
                CarID = dto.CarID,
                DriverID = dto.DriverID,
                date = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, egyptTimeZone),


            };
            await _context.AddAsync(driving);
            _context.SaveChanges();

            return Ok(driving); 
            }

            catch (Exception ex)
            {
                return BadRequest("this driver can not assign to this car ");
            }
        }



    }
}
