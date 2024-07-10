using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestGP.Models;

namespace TestGP.Controllers
{
   
        [EnableCors("AllowOrigin")]
        [Route("api/[controller]")]
        [ApiController]

        public class CurrentLocationController : ControllerBase
        {
            //   private readonly IHttpContextAccessor _httpContextAccessor;
            private readonly AppDbContext _context;
            public CurrentLocationController(AppDbContext context)//, IHttpContextAccessor httpContextAccessor)
            {
                _context = context;
                // _httpContextAccessor = httpContextAccessor;
            }






        [HttpGet]
        public async Task<IActionResult> getCurrentLocation()
        {

            return Ok();
        }
    }


    }