using ImageHandling.Dtos;
using ImageHandling.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestGP.DTO;
using TestGP.Models;

namespace TestGP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationTestController : ControllerBase
    {
        private readonly AppDbContext _context;
        public LocationTestController(AppDbContext context)//, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            // _httpContextAccessor = httpContextAccessor;
        }
        TimeZoneInfo egyptTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");

        [HttpPost]
        public async Task<IActionResult> addLocation([FromBody] postTestLocationdto dto)
        {
            var location = new TestLocation()
            {
                lat = dto.lat,
                lon = dto.lon,
                CarId=dto.CarId,
                date = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, egyptTimeZone),
            };

            _context.TestLocation.Add(location);
            _context.SaveChanges();
            return Ok(location);
        }
        [HttpGet]
        public async Task<IActionResult> getAll()
        {
            var all = await _context.TestLocation.ToListAsync();
            return Ok(all);
        }
        [HttpGet("{carId}")]
        public async Task<IActionResult> GetAll(int carId)
        {
            try
            {
                var lastTestLocation = await _context.TestLocation.Where(s=>s.CarId==carId)
                    .Include(t => t.Car).OrderByDescending(ts=>ts.Id)
                    .FirstOrDefaultAsync();

                if (lastTestLocation == null)
                {
                    return NotFound($"No test locations found for the CarId: {carId}");
                }

                return Ok(lastTestLocation);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }



    }
}
