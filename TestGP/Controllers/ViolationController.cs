using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System;
using TestGP.DTO;
using TestGP.Models;
using Microsoft.AspNetCore.Cors;
using ImageHandling.Helper;  

namespace TestGP.Controllers
{
    [EnableCors("AllowOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class ViolationController : Controller
    {
        private readonly AppDbContext _context;
        //private readonly IHttpContextAccessor _httpContextAccessor;

        public ViolationController(AppDbContext context)//, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            //_httpContextAccessor = httpContextAccessor;
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
                    var all = await _context.Violations.ToListAsync();
                    return Ok(all);
                }
                else
                {
                    return NotFound();
                }
            }
        }
        [HttpGet("byComp")]
        public async Task<IActionResult> getAllViolationbyComId(int id, string _userRole, int _userId)
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
                    var Violations = from violation in _context.Violations
                                     join driver in _context.Drivers on violation.driverID equals driver.Id
                                     where driver.CompID == id
                                     select new
                                     {
                                         violation.Id,
                                         violation.driverID,
                                         violation.type,
                                         violation.date
                                     };

                    return Ok(Violations);

                }
                else if (UserRole == "Manager")
                {
                    var Violations = from violation in _context.Violations
                                     join driver in _context.Drivers on violation.driverID equals driver.Id
                                     where driver.CompID == id && driver.MgrID == userId
                                     select new
                                     {
                                         violation.Id,
                                         violation.driverID,
                                         violation.type,
                                         violation.date
                                     };

                    return Ok(Violations);

                }
                else if (UserRole == "Admin")
                {
                    var Violations = from violation in _context.Violations
                                     join driver in _context.Drivers on violation.driverID equals driver.Id
                                     where driver.CompID == id && driver.AdminID == userId
                                     select new
                                     {
                                         violation.Id,
                                         violation.driverID,
                                         violation.type,
                                         violation.date
                                     };

                    return Ok(Violations);

                }
                else if (UserRole == "Assistant")
                {
                    var Violations = from violation in _context.Violations
                                     join driver in _context.Drivers on violation.driverID equals driver.Id
                                     where driver.CompID == id && driver.AssID == userId
                                     select new
                                     {
                                         violation.Id,
                                         violation.driverID,
                                         violation.type,
                                         violation.date
                                     };

                    return Ok(Violations);

                }
                else
                {
                    return NotFound();
                }
            }
        }
        [HttpPost]
        //raspberry pi
        public async Task<IActionResult> postViolationType([FromForm] postViolationdto dto )
        {

            var UserRole = "SuperAdmin";
            var userId = 1;
           
            var driver = await _context.Drivers.FindAsync(dto.driverID);

            var violatioType = dto.type;
            var databaseType = "";
            if (violatioType.Contains("no_seat_belt"))
            {
                driver.Salary = driver.Salary - 50;
                databaseType += "Seat belt violation, ";
                _context.SaveChanges();

            }
            if (violatioType.Contains("eating_drinking"))
            {
                driver.Salary = driver.Salary - 50;
                databaseType += "Eating or drinking violation ,";
                _context.SaveChanges();

            }
            if (violatioType.Contains("cell_phone"))
            {
                driver.Salary = driver.Salary - 50;
                databaseType += "cell phone violation ,";
                _context.SaveChanges();


            }
            if (violatioType.Contains("drowsy"))
            {
                driver.Salary = driver.Salary - 50;
                databaseType += "Drowsy violation ";
                _context.SaveChanges();

            }
            /*if (violatioType.Contains("speed"))
            {
                driver.Salary = driver.Salary - 50;
                _context.SaveChanges();


            }*/


            if (UserRole == null)
            {
                return NotFound("Session data not found.");
            }
            else
            {
                if (UserRole == "SuperAdmin")
                {
                    var violation = new Violation()
                    {
                        type = databaseType,
                        date = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, egyptTimeZone),
                        driverID = 1,//dto.
                        Img = await DocumentSettings.UploadFileAsync(dto.Img, "DriverViolation"),
                        CarID=2,
                        
                        /*lat="N/A",
                        lon="N/A",
                        */
                    };
                   
                    await _context.AddAsync(violation);
                    _context.SaveChanges();
                    return Ok(violation );

                }
                else
                {
                    return NotFound();
                }
            }
        }
        TimeZoneInfo egyptTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
        [HttpPost("speed")]
        public async Task<IActionResult> postViolationSpeed([FromBody]postViolationSpeedDto dto)
        {
            var violation = new Violation()
            {
                type = dto.type,
                date = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, egyptTimeZone),
                driverID = dto.driverID,
                CurrentSpeed = dto.CurrentSpeed,
                CarID = dto.CarID,
                /*lat = dto.lat,
                lon = dto.lon,
                */Load=dto.Load,
                RPM=dto.RPM,



            };
            var driver = await _context.Drivers.FindAsync(dto.driverID);
            driver.Salary -= 50;

            await _context.AddAsync(violation);
            _context.SaveChanges();


            return Ok(new { Violation=violation , Driversalary=driver.Salary });
        }

      /* [HttpPost("track")]
         public async Task<IActionResult> track(int? num,string? latitude, string? longitude)
         {
             var lat =latitude;
             var lon = longitude;
             if(num==1)
             {
                 return Ok (new { Lat = lat, Lon = lon });

             }
             else
             {

             return Ok(num);
             }
         }*/
      /*  [HttpPost("postLocation")]
          public async Task<IActionResult> PostLocation(string latitude, string longitude)
          {
              return Ok(new { Latitude = latitude, Longitude = longitude });
          }

          [HttpPost("track")]
          public async Task<IActionResult> Track(int? num)
          {
              if (num == 1)
              {
                  string latitude = "12.3456";
                  string longitude = "78.9101";
                  return await PostLocation(latitude, longitude);
              }
              else
              {
                  return BadRequest("Invalid request parameters.");
              }
          }*/
      /*  [HttpPost("track")]
        public async Task<IActionResult> Track(int? num)
        {
            if (num == 1)
            {
                // Call the PostLocation method to retrieve latitude and longitude
                var locationResult = await PostLocation();

                // Check if the PostLocation call was successful
                if (locationResult is OkObjectResult okResult && okResult.Value is { } locationData)
                {
                    // Extract latitude and longitude from the result
                    string latitude = locationData.Latitude;
                    string longitude = locationData.Longitude;

                    // Return latitude and longitude
                    return Ok(new { Latitude = latitude, Longitude = longitude });
                }
                else
                {
                    // If retrieving latitude and longitude failed, return an error
                    return BadRequest("Failed to retrieve latitude and longitude.");
                }
            }
            else
            {
                return BadRequest("Invalid request parameters.");
            }
        }

        // This method is used internally to obtain latitude and longitude
        private async Task<IActionResult> PostLocation()
        {
            // Simulate the retrieval of latitude and longitude from the embedded system
            // For demonstration purposes, let's assume latitude and longitude are hardcoded
            string latitude = "12.3456";
            string longitude = "78.9101";

            // Return latitude and longitude
            return Ok(new { Latitude = latitude, Longitude = longitude });
        }
*/
      
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
                if (UserRole == "Manager" || UserRole == "Admin")
                {
                    var violation = await _context.Violations.Include(v=>v.driver).SingleOrDefaultAsync(g => g.Id == id && (g.driver.MgrID == userId || g.driver.AdminID == userId));
                    if (violation == null)
                        return NotFound($" no violation was found with ID: {id} ");
                    _context.Remove(violation);
                    _context.SaveChanges();
                    return Ok("Deleted Successfully :)");

                }
                else
                {
                    return NotFound();
                }
            }
        }
        [HttpGet("byDriverId")]
        public async Task<IActionResult> getAllViolationOfDriverIdAndComId(int driverid, string _userRole, int _userId)
        {
            var UserRole = _userRole;
            var userId = _userId;

            if (UserRole == null)
            {
                return NotFound("Session data not found.");
            }
            else
            {
                if (UserRole == "Manager" || UserRole == "Admin" || UserRole == "Assistant" || UserRole == "Driver")
                {
                    /* var Violations = from violation in _context.Violations
                                      join driver in _context.Drivers on violation.driverID equals driver.Id
                                      where violation.driverID == driverid 
                                      select violation;*/

                    var Violations = await _context.Violations.Where(
                        v => v.driverID == driverid && (v.driver.MgrID == userId || v.driver.AdminID == userId || v.driver.AssID == userId || v.driverID == userId)).ToListAsync();
                    if (Violations is null)
                        return Ok("No Violations");

                    return Ok(Violations);

                }
                else
                {
                    return NotFound();
                }
            }
        }



        [HttpGet("byMgr")]
        public async Task<IActionResult> ViolationByManager(string _userRole, int _userId)
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
                    /* var Violations = from violation in _context.Violations
                                      join driver in _context.Drivers on violation.driverID equals driver.Id
                                      where violation.driverID == driverid 
                                      select violation;*/

                    var Violations = await _context.Violations.Where(v => v.driver.MgrID == userId).ToListAsync();
                    if (Violations is null)
                        return Ok("No Violations");

                    return Ok(Violations);

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
                string imageUrl = DocumentSettings.GetImageUrl(imageName, "DriverViolation"); // Assuming you have a function to get the image URL
                return Ok(imageUrl);
            }
            return Ok("NotFound");
        }
    }
}