using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PasswordHandlling.Dto;
using PasswordHandlling.Models;
using PasswordHandlling.Services.EmailSettings;
using System.Threading.Tasks;
using TestGP.Models;

namespace TestGP.Controllers
{

    [EnableCors("AllowOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class userController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IEmailSender _emailSender;
        private readonly IConfiguration _configuration;
        // private readonly IHttpContextAccessor _httpContextAccessor;

        public userController(AppDbContext dbContext, IEmailSender emailSender, IConfiguration configuration)
        {
            _context = dbContext;
            _emailSender = emailSender;
            _configuration = configuration;

        }

        [HttpPost("send-reset-password-mail")]
        public async Task<IActionResult> SendResetPassword(string email)
        {

            //var user = await _context.users.FirstOrDefaultAsync(u => u.Email == email);
            var adminUser = await _context.Admins.FirstOrDefaultAsync(u => u.Email == email);
            var assistantUser = await _context.Assistants.FirstOrDefaultAsync(u => u.Email == email);
            var superUser = await _context.SuperAdminCompany.FirstOrDefaultAsync(u => u.Email == email);
            var managerUser = await _context.Managers.FirstOrDefaultAsync(u => u.Email == email);
            var driverUser = await _context.Drivers.FirstOrDefaultAsync(u => u.Email == email);



            if (adminUser != null|| assistantUser != null || superUser != null || managerUser != null || driverUser != null)
            {
                var token = Guid.NewGuid().ToString();
                var expiration = DateTime.UtcNow.AddHours(1);

                _context.passwordResetTokens.Add(new PasswordResetToken
                {
                    Email = email,
                    Token = token,
                    Expiration = expiration
                });
                await _context.SaveChangesAsync();

                var resetPasswordUrl = $"http://localhost:4200/forgetpassword?token={token}";
                await _emailSender.SendAsync(
                    from: _configuration["EmailSettings:SenderEmail"],
                    recipients: email,
                    subject: "Reset Your Password",
                    body: resetPasswordUrl);

                return Ok("Password reset link sent.");
            }
            return Ok("No user found with this email.");
        }



        /////////////////////////////////////////////////////////////
        [HttpPut]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto model)
        {

            var tokenRecord = await _context.passwordResetTokens.FirstOrDefaultAsync(t => t.Token == model.Token && t.Expiration > DateTime.UtcNow);
            if (tokenRecord != null)
               {
            
                     var adminUser = await _context.Admins.FirstOrDefaultAsync(u => u.Email == tokenRecord.Email);
                     var assistantUser= await _context.Assistants.FirstOrDefaultAsync(u => u.Email == tokenRecord.Email);
                     var superUser= await _context.SuperAdminCompany.FirstOrDefaultAsync(u => u.Email == tokenRecord.Email);
                     var managerUser= await _context.Managers.FirstOrDefaultAsync(u => u.Email == tokenRecord.Email);
                     var driverUser= await _context.Drivers.FirstOrDefaultAsync(u => u.Email == tokenRecord.Email);
                    if (adminUser!=null){
                    	adminUser.Password = model.NewPassword;
                      _context.passwordResetTokens.Remove(tokenRecord);
                      await _context.SaveChangesAsync();
                      return Ok("Password has been reset successfully.");
                    }

                if (assistantUser != null)
                {
                    assistantUser.Password = model.NewPassword;
                    _context.passwordResetTokens.Remove(tokenRecord);
                    await _context.SaveChangesAsync();
                    return Ok("Password has been reset successfully.");
                }
                if (superUser != null)
                {
                    superUser.Password = model.NewPassword;
                    _context.passwordResetTokens.Remove(tokenRecord);
                    await _context.SaveChangesAsync();
                    return Ok("Password has been reset successfully.");
                }

                if (managerUser != null)
                {
                    managerUser.Password = model.NewPassword;
                    _context.passwordResetTokens.Remove(tokenRecord);
                    await _context.SaveChangesAsync();
                    return Ok("Password has been reset successfully.");
                }

                if (driverUser != null)
                {
                    driverUser.Password = model.NewPassword;
                    _context.passwordResetTokens.Remove(tokenRecord);
                    await _context.SaveChangesAsync();
                    return Ok("Password has been reset successfully.");
                }
            }
            return NotFound("Invalid or expired token.");
        }









        [HttpPost]
        public async Task<IActionResult> authenticateUser([FromBody] user user)
        {
            if (user == null || string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password))
            {
                return BadRequest("Email and password are required.");
            }

            // check if the user exist in the Admin table
            var admin = await _context.Admins.FirstOrDefaultAsync(a => a.Email == user.Email && a.Password == user.Password);
            if (admin != null)
            {
                // Create session
                /*_httpContextAccessor.HttpContext.Session.SetString("UserRole", "Admin");
                _httpContextAccessor.HttpContext.Session.SetInt32("Id", admin.Id);*/
                return Ok(new { message = "Admin authentication successful.", user = admin,role="Admin" });
            }

            // check if the user exist in the Assistant table
            var assistant = await _context.Assistants.FirstOrDefaultAsync(a => a.Email == user.Email && a.Password == user.Password);
            if (assistant != null)
            {
                /*_httpContextAccessor.HttpContext.Session.SetString("UserRole", "Assistant");
                _httpContextAccessor.HttpContext.Session.SetInt32("Id", assistant.Id);*/
                return Ok(new { message = "Assistant authentication successful.", user = assistant ,role ="Assistant"});
            }

            // check if the user exist in the Driver table
            var driver = await _context.Drivers.FirstOrDefaultAsync(d => d.Email == user.Email && d.Password == user.Password);
            if (driver != null)
            {
               /* _httpContextAccessor.HttpContext.Session.SetString("UserRole", "Driver");
                _httpContextAccessor.HttpContext.Session.SetInt32("Id", driver.Id);*/
                return Ok(new { message = "Driver authentication successful.", user = driver, role = "Driver" });
            }

            // check if the user exist in the Manager table
            var manager = await _context.Managers.FirstOrDefaultAsync(m => m.Email == user.Email && m.Password == user.Password);
            if (manager != null)
            {
               /* _httpContextAccessor.HttpContext.Session.SetString("UserRole", "Manager");
                _httpContextAccessor.HttpContext.Session.SetInt32("Id", manager.Id);*/
                return Ok(new { message = "Manager authentication successful.", user = manager, role = "Manager" });
            }

            // check if the user exist in the super table
            var super = await _context.SuperAdminCompany.FirstOrDefaultAsync(m => m.Email == user.Email && m.Password == user.Password);
            if (super != null)
            {
               /* _httpContextAccessor.HttpContext.Session.SetString("UserRole", "SuperAdmin");
                _httpContextAccessor.HttpContext.Session.SetInt32("Id", super.Id);
               */ 
                return Ok(new { message = "Super authentication successful.", user = super, role = "SuperAdmin" });
            }

            // check if the user exist in the applicants table
            /*var applicant = await _context.Applicants.FirstOrDefaultAsync(m => m.Email == user.Email && m.Password == user.Password);
            if (applicant != null)
            {
               *//* _httpContextAccessor.HttpContext.Session.SetString("UserRole", "Applicant");
                _httpContextAccessor.HttpContext.Session.SetInt32("Id", applicant.Id);*//*
                return Ok(new { message = "Applicant authentication successful.", user = applicant, role = "Applicant" });
            }
*/
            return Unauthorized("Invalid email or password.");
        }
        #region Test Session
        /* 
        [HttpGet("testSession")]
       * public async Task<IActionResult> GetSessionData()
        {
            // Retrieve session data
            var UserRole = _httpContextAccessor.HttpContext.Session.GetString("UserRole");

            if (UserRole == null)
            {
                return NotFound("Session data not found.");
            }

            var Id = _httpContextAccessor.HttpContext.Session.GetInt32("Id");
            return Ok($"User Role from session: {UserRole} , ID : {Id}");
        }*/

        #endregion
        
        [HttpGet("alertToSuperAdminSD")]
        public async Task<IActionResult> alertForSubscriptionDate_SuperAdmin(string _userRole, int _userId)
        {
            // Retrieve session data
            var UserRole = _userRole;
            var userId = _userId;
            var managerNotSubsribed = new List<Manager>();
            if (UserRole == null)
            {
                return NotFound("Session data not found.");
            }
            else
            {
                if (UserRole == "SuperAdmin")
                {
                    var manager = await _context.Managers.Where(m => m.SuperAdminId == userId).ToListAsync();
                    for (int i = 0;i<manager.Count();i++)
                    {

                        if (manager[i].Subcribtion == "for month")
                        {
                                if (DateTime.Today.Date == manager[i].DateOfSubscribtion.Date.AddMonths(1))
                                {
                                    managerNotSubsribed.Add(manager[i]);
                                }
                        }
                        else
                        {
                                if (DateTime.Today.Date == manager[i].DateOfSubscribtion.Date.AddYears(1))
                                {
                                    managerNotSubsribed.Add(manager[i]);
                                }
                        }
                    }

                    if (managerNotSubsribed != null)
                    {
                        return Ok(managerNotSubsribed);
                    }
                    else
                    {
                        return Ok("not today :)");
                    }

                }
                else
                {
                    return NotFound("UserRole not a manager");
                }
            }
        }
        
        [HttpGet("alertToManagerSD")]
        public async Task<IActionResult> alertForSubscriptionDate_Manger(string _userRole, int _userId)
        {
            // Retrieve session data
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
                    var manager = await _context.Managers.FirstOrDefaultAsync(m => m.Id == userId);

                    if (manager.Subcribtion == "for month")
                    {
                        if (DateTime.Today.Date == manager.DateOfSubscribtion.Date.AddMonths(1))
                        {
                            return Ok("Please schedule for pay or our servise stoped !");
                        }
                        else
                        {
                            return Ok("subscription day is not approaching.");
                        }
                    }
                    else
                    {
                        if (DateTime.Today.Date == manager.DateOfSubscribtion.AddYears(1))
                        {
                            return Ok("Please schedule for pay or our servise stoped !");
                        }
                        else
                        {
                            return Ok("subscription day is not approaching.");
                        }
                    }
                }
                else
                {
                    return NotFound("UserRole not a manager");
                }
            }
        }


        [HttpGet("alertToDriverForLExD")]
        public async Task<IActionResult> alertForExpiredLicance_Driver(string _userRole, int _userId)
        {
            // Retrieve session data
            var UserRole = _userRole;
            var userId = _userId;

            if (UserRole == null)
            {
                return NotFound("Session data not found.");
            }
            else
            {
                if (UserRole == "Driver")
                {
                    var driver = await _context.Drivers.FirstOrDefaultAsync(m => m.Id == userId);

                    if (driver.LicenseExpDate.Date == DateTime.Today.Date)
                    {
                        return Ok("Please renew your license because it has expired");
                    }
                    else
                    {
                        return Ok("You do not need to renew your license");
                    }
                }
                else
                {
                    return NotFound("UserRole not a driver");
                }
            }
        }


        [HttpGet("alertToAdminForCarExD")]
        public async Task<IActionResult> licanceExpiredForCar_Admin(string _userRole, int _userId)
        {
            var UserRole = _userRole;
            var userId = _userId;

            if (UserRole == null)
            {
                return NotFound("Session data not found.");
            }
            else
            {
                if (UserRole == "Admin")
                {
                    //var admin = await _context.Admins.FirstOrDefaultAsync(m => m.Id == userId);
                    var cars = await _context.Cars.Where(c => c.AdminId == userId).ToListAsync();
                    var CarsExp = new List<Car>();
                    for (int i = 0; i < cars.Count(); i++)
                    {
                        if (cars[i].LicensExpDate.Date == DateTime.Today.Date)
                        {
                            CarsExp.Add(cars[i]);
                        }

                    }
                    if (CarsExp != null)
                    {
                        return Ok(CarsExp);
                    }
                    else
                    {
                        return Ok("not today :)");
                    }

                }
                else
                {
                    return NotFound("UserRole not a Admin");
                }
            }
        }


        [HttpGet("alertToAssistant")]
        public async Task<IActionResult> alertForMaintanceDay_Assistnat(string _userRole, int _userId)
        {
            var UserRole = _userRole;
            var userId = _userId;

            if (UserRole == null)
            {
                return NotFound("Session data not found.");
            }
            else
            {
                if (UserRole == "Assistant")
                {

                    var car = await _context.carMaintenances.Include(i => i.Car).Where(c => c.Car.AssId == userId).ToListAsync();
                    var CarNeedMaintance = new List<carMaintenance>();
                    for (int i = 0; i < car.Count(); i++)
                    {
                        if (car[i].maintenanceDay.Date == DateTime.Now.Date)
                        {
                            CarNeedMaintance.Add(car[i]);
                            car[i].maintenanceDay.Date.AddMonths(3);

                        }
                        

                    }
                    if (CarNeedMaintance != null)
                    {
                        return Ok(CarNeedMaintance);

                    }
                    else
                    {
                        return Ok("not today :)");
                    }




                }
                else
                {
                    return NotFound("UserRole not a Driver");
                }
            }

        }


        /*
                [HttpPost("logout")]
                public IActionResult Logout()
                {
                    // Remove user-related data from session
                    _httpContextAccessor.HttpContext.Session.Clear();

                    return Ok("User logged out successfully.");
                }
        */


    }
}
/*

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TestGP.Models;

namespace TestGP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class userController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public userController(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] user user)
        {
            if (user == null || string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password))
            {
                return BadRequest("Email and password are required.");
            }

            // Find superAdminCompany in database
            var foundsuperAdminCompany = await _context.SuperAdminCompany.FirstOrDefaultAsync(u => u.Email == user.Email && u.Password == user.Password);
            if (foundsuperAdminCompany == null)
            {
                return Unauthorized("Invalid email or password.");
            }

            // Generate JWT token
            var token = GenerateJwtToken(foundsuperAdminCompany);

            return Ok(new { token });
        }

        private string GenerateJwtToken(SuperAdminCompany superAdminCompany)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Secret"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, superAdminCompany.Email),
                    new Claim(ClaimTypes.Role, "SuperAdmin"), // Assuming role is stored in the superAdminCompany object
                    new Claim("userId", superAdminCompany.Id.ToString()) // Assuming Id is stored in the superAdminCompany object
                }),
                Expires = DateTime.UtcNow.AddDays(1), // Token expiration time
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }


        [HttpGet]
        [Authorize]
        public IActionResult GetSecureData()
        {
            // Retrieve user information from claims
            var userId = User.FindFirst("userId")?.Value;
            var userEmail = User.Identity.Name;
            
            // You can perform any action here, for example, return user-specific data
            return Ok(new
            {
                userId = userId,
                Email = userEmail,
                Message = "This is secure data. You are authenticated!"
            });
        }

        // Other action methods...
    }
}
*/