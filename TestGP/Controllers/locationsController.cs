/*using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestGP.DTO;
using TestGP.Models;

namespace TestGP.Controllers
{
    [EnableCors("AllowOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly AppDbContext _context;

        public LocationController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLocations(string _userRole, int _userId)
        {
            if (_userRole == null)
            {
                return NotFound("User role not provided.");
            }
            else
            {
                if (_userRole == "SuperAdmin" || _userRole == "Manager" || _userRole == "Admin" || _userRole == "Assistant")
                {
                    var locations = await _context.Locations.ToListAsync();
                    return Ok(locations);
                }
                else
                {
                    return NotFound("Unauthorized access.");
                }
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateLocation([FromBody] postLocationdto dto, string _userRole, int _userId)
        {
            if (_userRole == null)
            {
                return NotFound("User role not provided.");
            }
            else
            {
                if (_userRole == "SuperAdmin" || _userRole == "Manager" || _userRole == "Admin")
                {
                    var location = new Location
                    {

                        NormalSpeed = dto.NormalSpeed,
                       *//* lat = dto.lat,
                        lon = dto.lon,
                        date= dto.date,*//*
                    };

                    await _context.Locations.AddAsync(location);
                    await _context.SaveChangesAsync();
                    return Ok(location);
                }
                else
                {
                    return NotFound("Unauthorized access.");
                }
            }
        }

        [HttpGet("byId")]
        public async Task<IActionResult> GetCarById(int id, string _userRole, int _userId)
        {
            if (_userRole == null)
            {
                return NotFound("User role not provided.");
            }
            else
            {
                if (_userRole == "SuperAdmin" || _userRole == "Manager" || _userRole == "Admin")
                {
                    var car = await _context.Cars.FindAsync(id);
                    if (car == null)
                    {
                        return NotFound($"Car with ID {id} not found.");
                    }
                    return Ok(car);
                }
                else
                {
                    return NotFound("Unauthorized access.");
                }
            }
        }


        [HttpPut]
        public async Task<IActionResult> UpdateLocation(int id, [FromBody] postLocationdto dto, string _userRole, int _userId)
        {
            if (_userRole == null)
            {
                return NotFound("User role not provided.");
            }
            else
            {
                if (_userRole == "SuperAdmin" || _userRole == "Manager" || _userRole == "Admin")
                {
                    var location = await _context.Locations.FindAsync(id);
                    if (location == null)
                    {
                        return NotFound($"Location with ID {id} not found.");
                    }
                    location.NormalSpeed = dto.NormalSpeed;
                    *//*location.lat = dto.lat;
                    location.lon = dto.lon;
                    location.date = dto.date;
                    *//*_context.Locations.Update(location);
                    await _context.SaveChangesAsync();

                    return Ok(location);
                }
                else
                {
                    return NotFound("Unauthorized access.");
                }
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteLocation(int id, string _userRole, int _userId)
        {
            if (_userRole == null)
            {
                return NotFound("User role not provided.");
            }
            else
            {
                if (_userRole == "SuperAdmin" || _userRole == "Manager" || _userRole == "Admin")
                {
                    var location = await _context.Locations.FindAsync(id);
                    if (location == null)
                    {
                        return NotFound($"Location with ID {id} not found.");
                    }

                    _context.Locations.Remove(location);
                    await _context.SaveChangesAsync();

                    return Ok("Location deleted successfully.");
                }
                else
                {
                    return NotFound("Unauthorized access.");
                }
            }
        }
    }
}
*/