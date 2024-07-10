/*using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TestGP.DTO;
using TestGP.Models;

namespace TestGP.Controllers
{
    [EnableCors("AllowOrigin")]
    [Route("api/[controller]")]
    [ApiController]

    public class ManageViolationController : ControllerBase
    {
        //private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AppDbContext _context;

        public ManageViolationController(AppDbContext context)//, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            // _httpContextAccessor = httpContextAccessor;
        }
        [HttpGet]
        public async Task<IActionResult> getAllViolation(string _userRole, int _userId)
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
                    var all = await _context.ManageViolations.ToListAsync();
                    return Ok(all);
                }
                else
                {
                    return NotFound();
                }
            }
        }
        [HttpPost]
        [Route("Violationspeed")]
        public async Task<IActionResult> GetViolationspeed([FromBody] postManageViolationdto dto)

        {

            var UserRole = "SuperAdmin";
            var userId = 1;

            if (UserRole == null)
            {
                return NotFound("Session data not found.");
            }
            else
            {
                if (UserRole == "SuperAdmin")
                {


                    var Violation = new Violation()
                    {
                        type = "Speed",
                        driverID = dto.DriverID,
                        date = dto.DateOfViolation,

                    }
                    ;
                    var driver = await _context.Drivers.FindAsync(dto.DriverID);
                    driver.Salary = driver.Salary - 50;

                    await _context.AddAsync(Violation);



                    _context.SaveChanges();
                    var ManageViolation = new ManageViolation()
                    {
                        DriverID = dto.DriverID,
                        CarID = dto.CarID,
                        CurrentSpeed = dto.CurrentSpeed,
                        DateOfViolation = dto.DateOfViolation,
                        LocID = dto.LocID,
                        ViolationID = Violation.Id,
                    };
                    await _context.AddAsync(ManageViolation);
                    _context.SaveChanges();
                    return Ok(ManageViolation);
                }


                else
                {
                    return Ok("No Violation");
                }
            }
                else
            {
                return NotFound();
            }
        }



    }




    [HttpPut]
    public async Task<IActionResult> update(int DriverID, int ViolationID, DateTime DateOfViolation, postManageViolationdto dto, string _userRole, int _userId)
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
                var ManageViolations = await _context.ManageViolations.SingleOrDefaultAsync(g => g.DriverID == DriverID && g.ViolationID == ViolationID && g.DateOfViolation == DateOfViolation);
                if (ManageViolations == null)
                    return NotFound($" no ManageViolations was found ");
                ManageViolations.DriverID = dto.DriverID;
                ManageViolations.CarID = dto.CarID;
                ManageViolations.LocID = dto.LocID;
                ManageViolations.ViolationID = dto.ViolationID;
                ManageViolations.CurrentSpeed = dto.CurrentSpeed;
                ManageViolations.DateOfViolation = dto.DateOfViolation;


                _context.SaveChanges();
                return Ok(ManageViolations);
            }
            else
            {
                return NotFound();
            }
        }

    }

    [HttpDelete]
    public async Task<IActionResult> delete(int DriverID, int ViolationID, DateTime DateOfViolation, string _userRole, int _userId)
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
                var ManageViolations = await _context.ManageViolations.SingleOrDefaultAsync(g => g.DriverID == DriverID && g.ViolationID == ViolationID && g.DateOfViolation == DateOfViolation);
                if (ManageViolations == null)
                    return NotFound($" no ManageViolations was found  ");
                _context.Remove(ManageViolations);
                _context.SaveChanges();
                return Ok("Deleted Successfully :)");
            }
            else
            {
                return NotFound();
            }
        }

    }


}
}*/