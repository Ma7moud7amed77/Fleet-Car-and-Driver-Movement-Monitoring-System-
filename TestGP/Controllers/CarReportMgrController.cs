using Microsoft.AspNetCore.Cors;
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
    public class CarReportMgrController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CarReportMgrController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("byMgr")]
        public async Task<IActionResult> GenerateReport(string _userRole, int _userId)
        {

            var userRole = _userRole;
            var userId = _userId;
            try
            {
                if (userRole == null)
                {
                    return NotFound("User role not provided.");
                }
                else if (userRole != "Manager")
                {
                    return NotFound("Unauthorized access.");
                }

                var manager = await _context.Managers.FindAsync(userId);
                if (manager == null)
                {
                    return NotFound($"Manager with ID {userId} not found.");
                }

                var carsOfSpecificManager = await _context.Cars
                    .Where(c => c.MgrID == userId)
                    .ToListAsync();

                using (var excelPackage = new ExcelPackage())
                {
                    var worksheet = excelPackage.Workbook.Worksheets.Add("Report all car by manager");

                    worksheet.Cells[1, 1].Value = "Car ID";
                    worksheet.Cells[1, 2].Value = "Car Name";
                    worksheet.Cells[1, 3].Value = "License";
                    worksheet.Cells[1, 4].Value = "License EXP";
                    worksheet.Cells[1, 5].Value = "Maintenance Day";
                    worksheet.Cells[1, 6].Value = "Remaining Days to Maintain";
                    worksheet.Cells[1, 7].Value = "Assistant ID";

                    for (int i = 0; i < carsOfSpecificManager.Count; i++)
                    {
                        var car = carsOfSpecificManager[i];
                        var maintenance = await _context.carMaintenances
                            .Where(m => m.carId == car.Id)
                            .OrderByDescending(m => m.maintenanceDay)
                            .FirstOrDefaultAsync();

                        var assistant = await _context.Assistants.FindAsync(car.AssId);

                        worksheet.Cells[i + 2, 1].Value = car.Id;
                        worksheet.Cells[i + 2, 2].Value = car.Name;
                        worksheet.Cells[i + 2, 3].Value = car.License;
                        worksheet.Cells[i + 2, 4].Value = car.LicensExpDate.ToString("MM/dd/yyyy");
                        worksheet.Cells[i + 2, 5].Value = maintenance?.maintenanceDay.ToString("MM/dd/yyyy");
                        worksheet.Cells[i + 2, 6].Value = $"{(maintenance?.maintenanceDay.Date.Day - DateTime.Now.Date.Day)}" + " Days";
                        worksheet.Cells[i + 2, 7].Value = car.AssId;
                    }

                    var excelBytes = excelPackage.GetAsByteArray();
                    return File(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Report.xlsx");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error generating report: {ex.Message}");
            }
        }
    }
}
