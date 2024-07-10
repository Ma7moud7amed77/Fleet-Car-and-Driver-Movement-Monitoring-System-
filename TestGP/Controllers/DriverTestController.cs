using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestGP.Models;
using Microsoft.AspNetCore.Cors;
using ImageHandling.Dtos;
using ImageHandling.Models;
using ImageHandling.Helper;

namespace TestGP.Controllers
{ 
	[EnableCors("AllowOrigin")]
	[Route("api/[controller]")]
	[ApiController]
	public class DriverTestController : ControllerBase
	{
		private readonly AppDbContext _context;
		public DriverTestController(AppDbContext context)//, IHttpContextAccessor httpContextAccessor)
		{
			_context = context;
			// _httpContextAccessor = httpContextAccessor;
		}
		[HttpPost]
		public async Task<IActionResult> CreateDriverAsync([FromForm] DriverTestDto driver)
		{
			var mappedDriver = new DriverTest()
			{
				Name = "test",
				Img = "null"
			};
			_context.DriverTest.Add(mappedDriver);
			_context.SaveChanges();
			mappedDriver.Img = await DocumentSettings.UploadFileAsync(driver.Img, "DriversImages");
			_context.SaveChanges();
			return Ok("Done");
		}
		[HttpGet]
		public IActionResult GetAll()
		{
			var drivers = _context.DriverTest.ToList();
			return Ok(drivers);
		}
		[HttpDelete]
		public IActionResult delete(int id)
		{
			var driver = _context.DriverTest.Where(d => d.Id == id).FirstOrDefault();
			if (driver != null)
			{
				DocumentSettings.DeleteFile(driver.Img, "DriversImages");
				_context.DriverTest.Remove(driver);
				_context.SaveChanges();
				return Ok("Deleted");
			}
			return Ok($"no driver by this Id:{id}");
		}
		[HttpGet("getImage")]
		public IActionResult GetImageURL(string imageName)
		{
			if (imageName != null)
			{
				string imageUrl = DocumentSettings.GetImageUrl(imageName, "DriversImages"); // Assuming you have a function to get the image URL
				return Ok(imageUrl);
			}
			return Ok("NotFound");
		}
		
		[HttpPut]
		public async Task<IActionResult> EditAsync(IFormFile newImage, int id)
		{
			var driver = _context.DriverTest.Where(d => d.Id == id).FirstOrDefault();
			if (driver != null)
			{
				driver.Img = await DocumentSettings.EditFile(newImage, driver.Img, "DriversImages");
				_context.SaveChanges();
				return Ok("Done");
			}
			else
			{
				return Ok("NotFound");
			}
		}
	}
}
