 using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using TestGP.DTO;
using TestGP.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
namespace TestGP.Controllers
{
    [EnableCors("AllowOrigin")] 
    [Route("api/[controller]")]
    [ApiController]
    public class superAdminTreatWithManagerController : ControllerBase
    {
        private readonly AppDbContext _context;
        public superAdminTreatWithManagerController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> getAllTreatmants()
        {
            var all = await _context.SuperAdminTreateManagers.ToListAsync();
            return Ok(all);
        }
        [HttpPost]
        public async Task<IActionResult> postTreatmant(postSuperAdminTreateManagerdto DTO, string _userRole, int _userId)
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
                    var SuperAdminTreateManager = new SuperAdminTreateManager
                    {

                        SuperID = 1,
                        MgrID = userId,
                        MgrFeedback = DTO.MgrFeedback,
                        //SuperFeedback = DTO.SuperFeedback,
                        DateOfMgrFeedback = DateTime.Now,
                        //DateOfSuperFeedback = DTO.DateOfSuperFeedback
                    };

                    await _context.AddAsync(SuperAdminTreateManager);
                    _context.SaveChanges();
                    return Ok(SuperAdminTreateManager);
                }
                else
                {
                    return NotFound();
                }
            }

            
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> update(int id, [FromBody] postSuperAdminTreateManagerdto DTO)
        {
            var SuperAdminTreateManager = await _context.SuperAdminTreateManagers.SingleOrDefaultAsync(g => g.Id == id);
            if (SuperAdminTreateManager == null)
                return NotFound($" no feedback with ID: {id} ");
            
            //SuperAdminTreateManager.SuperID = DTO.SuperID;
            //SuperAdminTreateManager.MgrID = DTO.MgrID;
            //SuperAdminTreateManager.DateOfMgrFeedback = DTO.DateOfMgrFeedback;
            SuperAdminTreateManager.DateOfSuperFeedback = DateTime.Now;
           // SuperAdminTreateManager.MgrFeedback = DTO.MgrFeedback;
            SuperAdminTreateManager.SuperFeedback = DTO.SuperFeedback;
            _context.SaveChanges();
            return Ok(SuperAdminTreateManager);

        }

        [HttpDelete]
        public async Task<IActionResult> delete(int id)
        {
            var SuperAdminTreateManager = await _context.SuperAdminTreateManagers.SingleOrDefaultAsync(g => g.Id == id);
            if (SuperAdminTreateManager == null)
                return NotFound($" no feedback with ID: {id} ");
            _context.Remove(SuperAdminTreateManager);
            _context.SaveChanges();
            return Ok("Deleted Successfully :)");

        }
        
        [HttpGet("allFeedbackByMgr/{id}")]
        public async Task<IActionResult> getFeedbacksByMgrId(int id)
        {
            var SuperAdminTreateManager = await _context.SuperAdminTreateManagers.Where(g => g.MgrID == id).ToListAsync();
            if (SuperAdminTreateManager == null)
                return NotFound($" no feedback with ID: {id} ");
            return Ok(SuperAdminTreateManager);
        }

        [HttpGet("download")]
        public async Task<IActionResult> DownloadReport()
        {
            try
            {
                // Retrieve manager messages and their replies from the database
                var messages = await _context.SuperAdminTreateManagers
                                    .Include(m => m.Manager) // Ensure Manager navigation property is loaded
                                    .ToListAsync();

                // Create a new Excel package
                using (var excelPackage = new ExcelPackage())
                {
                    // Add a worksheet
                    var worksheet = excelPackage.Workbook.Worksheets.Add("Manager Messages");

                    // Set column headers
                    worksheet.Cells[1, 1].Value = "Manager ID";
                    worksheet.Cells[1, 2].Value = "Manager Name";
                    worksheet.Cells[1, 3].Value = "Message";
                    worksheet.Cells[1, 4].Value = "Time of Message";
                    worksheet.Cells[1, 5].Value = "Super Admin Reply";
                    worksheet.Cells[1, 6].Value = "Date of Reply";

                    // Fill data rows
                    for (int i = 0; i < messages.Count; i++)
                    {
                        var message = messages[i];
                        worksheet.Cells[i + 2, 1].Value = message.MgrID;
                        worksheet.Cells[i + 2, 2].Value = message.Manager?.Name ?? "N/A"; // Null-conditional and null-coalescing operators used here
                        worksheet.Cells[i + 2, 3].Value = message.MgrFeedback;
                        worksheet.Cells[i + 2, 4].Value = message.DateOfMgrFeedback?.ToString("MM/dd/yyyy HH:mm:ss") ?? "N/A"; // Null-conditional and null-coalescing operators used here
                        worksheet.Cells[i + 2, 5].Value = message.SuperFeedback;
                        worksheet.Cells[i + 2, 6].Value = message.DateOfSuperFeedback?.ToString("MM/dd/yyyy HH:mm:ss") ?? "N/A"; // Null-conditional and null-coalescing operators used here
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


    }
}
