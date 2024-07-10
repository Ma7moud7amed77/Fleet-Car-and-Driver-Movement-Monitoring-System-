using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TestGP.Models;

namespace TestGP.Controllers
{
    [EnableCors("AllowOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class carMaintenanceController : ControllerBase
    {
        
        private readonly AppDbContext _context;

        public carMaintenanceController(AppDbContext context)//, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            
        }

        [HttpGet]
        public async Task<IActionResult> getAllCarMaintenanceDateTime(string _userRole, int _userId)
        {
            var UserRole = _userRole;
            var MgrId = _userId;

            if (UserRole == null)
            {
                return NotFound("Session data not found.");
            }
            else
            {
                if (UserRole == "SuperAdmin")
                {
                    var all = await _context.carMaintenances.ToListAsync();
                    return Ok(all);
                }
                else if (UserRole == "Manager")
                {
                    var car = await _context.Cars.Include(c=>c.Company).Where(g => g.Company.MgrID == MgrId).ToListAsync();
                    if (car == null)
                        return NotFound($"No cars were found with this Manager ID: {MgrId}");
                    return Ok(car);
                }
                else
                {
                    return NotFound();
                }
            }
        }

        [HttpPost]
        public async Task<IActionResult> postcarMaintenanceDateTime(string _userRole, int _userId, [FromBody] carMaintenance DTO)
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
                    var carMaintenance = new carMaintenance
                    {
                        carId = DTO.carId,
                        maintenanceDay = DTO.maintenanceDay
                    };
                    await _context.AddAsync(carMaintenance);
                    await _context.SaveChangesAsync();
                    return Ok(carMaintenance);
                }
                else
                {
                    return NotFound();
                }
            }
        }

        [HttpPut]
        public async Task<IActionResult> update(int carId, DateTime maintenanceDay, [FromBody] carMaintenance DTO, string _userRole, int _userId)
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
                    var carMaintenance = await _context.carMaintenances.SingleOrDefaultAsync(g => g.carId == carId && g.maintenanceDay == maintenanceDay);
                    if (carMaintenance == null)
                        return NotFound($"No car maintenance day with car ID: {carId}");
                    carMaintenance.carId = DTO.carId;
                    carMaintenance.maintenanceDay = DTO.maintenanceDay;
                    await _context.SaveChangesAsync();
                    return Ok(carMaintenance);
                }
                else
                {
                    return NotFound();
                }
            }
        }

        [HttpDelete]
        public async Task<IActionResult> delete(int carId, DateTime maintenanceDay, string _userRole, int _userId)
        {
            var UserRole = _userRole;
            if (UserRole == null)
            {
                return NotFound("Session data not found.");
            }
            else
            {
                if (UserRole == "Manager" || UserRole == "Admin")
                {
                    var carMaintenance = await _context.carMaintenances.SingleOrDefaultAsync(g => g.carId == carId && g.maintenanceDay == maintenanceDay);
                    if (carMaintenance == null)
                        return NotFound($"No car maintenance day with car ID: {carId}");
                    _context.Remove(carMaintenance);
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
        public async Task<IActionResult> getmaintenanceDayBycarId(int id, string _userRole, int _userId)
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
                    var carMaintenances = await _context.carMaintenances.Where(g => g.carId == id).ToListAsync();
                    if (carMaintenances == null)
                        return NotFound($"No maintenance day for car ID: {id}");
                    return Ok(carMaintenances);
                }
                else
                {
                    return NotFound();
                }
            }
        }
    }
}
