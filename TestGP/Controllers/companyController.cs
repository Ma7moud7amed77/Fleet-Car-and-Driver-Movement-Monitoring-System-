using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Threading.Tasks;
using TestGP.DTO;
using TestGP.Models;

namespace TestGP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class companyController : ControllerBase
    {
        private readonly AppDbContext _context;

        public companyController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("test")]
        public async Task<IActionResult> test()
        {
            var all = await _context.Companies.ToListAsync();
            return Ok(all);
        }

        [HttpGet("count")]
        public async Task<IActionResult> CountAll(string _userRole, int _userId)
        {
            if (_userRole == null)
            {
                return NotFound("Session data not found.");
            }
            else
            {
                if (_userRole == "SuperAdmin")
                {
                    var count = await _context.Companies.CountAsync();
                    return Ok(count);
                }
                else if (_userRole == "Manager")
                {
                    var count = await _context.Companies.Where(c => c.MgrID == _userId).CountAsync();
                    return Ok(count);
                }
                else
                {
                    return NotFound();
                }
            }
        }

        [HttpGet]
        public async Task<IActionResult> getAllcompanies(string _userRole, int _userId)
        {
            if (_userRole == null)
            {
                return NotFound("Session data not found.");
            }
            else
            {
                if (_userRole == "SuperAdmin")
                {
                    var all = await _context.Companies.ToListAsync();
                    return Ok(all);
                }
                else if (_userRole == "Manager")
                {
                    var all = await _context.Companies.Where(c => c.MgrID == _userId).ToListAsync();
                    return Ok(all);
                }
                else
                {
                    return NotFound();
                }
            }
        }

        [HttpPost]
        public async Task<IActionResult> postcompany(string _userRole, int _userId, [FromBody] postCompanydto cdto)
        {
            if (_userRole == null)
            {
                return NotFound("Session data not found.");
            }
            else
            {
                if (_userRole == "SuperAdmin" || _userRole == "Manager")
                {
                    var company = new Company
                    {
                        Name = cdto.Name,
                        Phone = cdto.Phone,
                        Location = cdto.Location,
                        MgrID = cdto.MgrID,
                    };
                    await _context.AddAsync(company);
                    _context.SaveChanges();
                    return Ok(company);
                }
                else
                {
                    return NotFound();
                }
            }
        }

        [HttpPut]
        public async Task<IActionResult> update(int id, [FromBody] postCompanydto cdto, string _userRole, int _userId)
        {
            if (_userRole == null)
            {
                return NotFound("Session data not found.");
            }
            else
            {
                if (_userRole == "SuperAdmin" || _userRole == "Manager")
                {
                    var company = await _context.Companies.SingleOrDefaultAsync(g => g.Id == id && g.MgrID == _userId);
                    if (company == null)
                        return NotFound($" no company was found with ID: {id} ");
                    company.Name = cdto.Name;
                    company.Phone = cdto.Phone;
                    company.Location = cdto.Location;
                    company.MgrID = cdto.MgrID;
                    _context.SaveChanges();
                    return Ok(company);
                }
                else
                {
                    return NotFound();
                }
            }
        }

        [HttpDelete]
        public async Task<IActionResult> delete(int id, string _userRole, int _userId)
        {
            if (_userRole == null)
            {
                return NotFound("Session data not found.");
            }
            else
            {
                if (_userRole == "SuperAdmin" || _userRole == "Manager")
                {
                    var company = await _context.Companies.SingleOrDefaultAsync(g => g.Id == id && g.MgrID == _userId);
                    if (company == null)
                        return NotFound($" no company was found with ID: {id} ");
                    _context.Remove(company);
                    _context.SaveChanges();
                    return Ok("Deleted Successfully :)");
                }
                else
                {
                    return NotFound();
                }
            }
        }

        [HttpGet("byId")]
        public async Task<IActionResult> getcompanyById(int id, string _userRole, int _userId)
        {
            if (_userRole == null)
            {
                return NotFound("Session data not found.");
            }
            else
            {
                if (_userRole == "SuperAdmin" || _userRole == "Manager")
                {
                    var company = await _context.Companies.SingleOrDefaultAsync(g => g.Id == id && g.MgrID == _userId);
                    if (company == null)
                        return NotFound($" no company was found with ID: {id} ");
                    return Ok(company);
                }
                else
                {
                    return NotFound();
                }
            }
        }

        [HttpGet]
        [Route("byMgr")]
        public async Task<IActionResult> getByMangerId(string _userRole, int _userId)
        {
            if (_userRole == null)
            {
                return NotFound("Session data not found.");
            }
            else
            {
                if (_userRole == "Manager")
                {
                    var all = await _context.Companies.Where(m => m.MgrID == _userId).ToListAsync();
                    return Ok(all);
                }
                else
                {
                    return NotFound();
                }
            }
        }
    }
}
