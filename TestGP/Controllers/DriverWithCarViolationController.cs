using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System;
using System.Linq;
using System.Threading.Tasks;
using TestGP.Models;

namespace TestGP.Controllers
{
    [EnableCors("AllowOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class DriverWithCarViolationController : ControllerBase
    {
        //private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AppDbContext _context;

        public DriverWithCarViolationController(AppDbContext context)//, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
          //  _httpContextAccessor = httpContextAccessor;
        }
        [HttpGet("generate")]
        public async Task<IActionResult> GenerateReport(string _userRole, int _userId)
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
                    try
                    {
                        // Retrieve data directly from original tables
                        var allManageViolations = await _context.ManageViolations.Where(c => c.Driver.MgrID == userId).ToListAsync();

                        // Create a new Excel package
                        using (var excelPackage = new ExcelPackage())
                        {
                            // Add a worksheet
                            var worksheet = excelPackage.Workbook.Worksheets.Add("DriverWithCarViolation");

                            // Set column headers
                            worksheet.Cells[1, 1].Value = "Driver ID";
                            worksheet.Cells[1, 2].Value = "Driver Name";
                            worksheet.Cells[1, 3].Value = "Car ID";
                            worksheet.Cells[1, 4].Value = "Location";
                            worksheet.Cells[1, 5].Value = "Violation ID";
                            worksheet.Cells[1, 6].Value = "Violation Type";
                            worksheet.Cells[1, 7].Value = "Violation Time";
                            worksheet.Cells[1, 8].Value = "Assistant ID";
                            worksheet.Cells[1, 9].Value = "Assistant Name";
                            worksheet.Cells[1, 10].Value = "Admin ID";
                            worksheet.Cells[1, 11].Value = "Admin Name";
                            worksheet.Cells[1, 12].Value = "Driver Salary";

                            // Fill data rows
                            for (int i = 0; i < allManageViolations.Count; i++)
                            {
                                var manageViolation = allManageViolations[i];
                                var driver = await _context.Drivers.FindAsync(manageViolation.DriverID);
                                var violation = await _context.Violations.FindAsync(manageViolation.ViolationID);
                                var assistant = await _context.Assistants.FindAsync(driver.AssID);
                                var admin = await _context.Admins.FindAsync(driver.AdminID);
                                var car = await _context.Cars.FindAsync(manageViolation.CarID);
                                //var loc = await _context.Locations.FindAsync(manageViolation.LocID);

                                worksheet.Cells[i + 2, 1].Value = driver.Id;
                                worksheet.Cells[i + 2, 2].Value = driver.Name;
                                worksheet.Cells[i + 2, 3].Value = car.Id;
                               // worksheet.Cells[i + 2, 4].Value = loc.Address;
                                worksheet.Cells[i + 2, 4].Value = violation.Id;
                                worksheet.Cells[i + 2, 5].Value = violation.type;
                                worksheet.Cells[i + 2, 6].Value = manageViolation.DateOfViolation.ToString("MM/dd/yyyy HH:mm:ss");
                                worksheet.Cells[i + 2, 7].Value = assistant.Id;
                                worksheet.Cells[i + 2, 8].Value = assistant.Name;
                                worksheet.Cells[i + 2, 9].Value = admin.Id;
                                worksheet.Cells[i + 2, 10].Value = admin.Name;
                                worksheet.Cells[i + 2, 11].Value = driver.Salary;
                            }

                            // Convert the Excel package to a byte array
                            var excelBytes = excelPackage.GetAsByteArray();

                            // Return the Excel file as a byte array
                            return File(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Report.xlsx");
                        }
                    }
                    catch (Exception ex)
                    {
                        return StatusCode(StatusCodes.Status500InternalServerError, "Error generating report: " + ex.Message);
                    }


                else if (UserRole == "Admin")
                {
                    try
                    {
                        var admin = await _context.Admins.FirstOrDefaultAsync(g => g.Id == userId);
                        // Retrieve data directly from original tables
                        var allManageViolations = await _context.ManageViolations.Where(c => c.Driver.MgrID == admin.MgrID).ToListAsync();

                        // Create a new Excel package
                        using (var excelPackage = new ExcelPackage())
                        {
                            // Add a worksheet
                            var worksheet = excelPackage.Workbook.Worksheets.Add("DriverWithCarViolation");

                            // Set column headers
                            worksheet.Cells[1, 1].Value = "Driver ID";
                            worksheet.Cells[1, 2].Value = "Driver Name";
                            worksheet.Cells[1, 3].Value = "Car ID";
                            worksheet.Cells[1, 4].Value = "Location";
                            worksheet.Cells[1, 5].Value = "Violation ID";
                            worksheet.Cells[1, 6].Value = "Violation Type";
                            worksheet.Cells[1, 7].Value = "Violation Time";
                            worksheet.Cells[1, 8].Value = "Assistant ID";
                            worksheet.Cells[1, 9].Value = "Assistant Name";
                            worksheet.Cells[1, 10].Value = "Driver Salary";

                            // Fill data rows
                            for (int i = 0; i < allManageViolations.Count; i++)
                            {
                                var manageViolation = allManageViolations[i];
                                var driver = await _context.Drivers.FindAsync(manageViolation.DriverID);
                                var violation = await _context.Violations.FindAsync(manageViolation.ViolationID);
                                var assistant = await _context.Assistants.FindAsync(driver.AssID);
                                var admin02 = await _context.Admins.FindAsync(driver.AdminID);
                                var car = await _context.Cars.FindAsync(manageViolation.CarID);
                                //var loc = await _context.Locations.FindAsync(manageViolation.LocID);

                                worksheet.Cells[i + 2, 1].Value = driver.Id;
                                worksheet.Cells[i + 2, 2].Value = driver.Name;
                                worksheet.Cells[i + 2, 3].Value = car.Id;
                               // worksheet.Cells[i + 2, 4].Value = loc.Address;
                                worksheet.Cells[i + 2, 4].Value = violation.Id;
                                worksheet.Cells[i + 2, 5].Value = violation.type;
                                worksheet.Cells[i + 2, 6].Value = manageViolation.DateOfViolation.ToString("MM/dd/yyyy HH:mm:ss");
                                worksheet.Cells[i + 2, 7].Value = assistant.Id;
                                worksheet.Cells[i + 2, 8].Value = assistant.Name;
                                worksheet.Cells[i + 2, 9].Value = driver.Salary;
                            }

                            // Convert the Excel package to a byte array
                            var excelBytes = excelPackage.GetAsByteArray();

                            // Return the Excel file as a byte array
                            return File(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Report.xlsx");
                        }
                    }
                    catch (Exception ex)
                    {
                        return StatusCode(StatusCodes.Status500InternalServerError, "Error generating report: " + ex.Message);
                    }

                }
                else
                {
                    return NotFound();
                }
            }


        }
    }
}
    
