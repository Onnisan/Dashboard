using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Dashboard.Models;
using Dashboard.Data;

namespace Dashboard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhoneController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PhoneController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetAllPhone")]
        public JsonResult GetPhone()
        {
            var PhoneData = _context.ProductDetails.ToList();
            if (PhoneData == null)
            {
                return new JsonResult("Not found");
            }
            return new JsonResult(Ok(PhoneData));
        }
        [HttpPost("GetAllPhone/{id}")]
        public JsonResult GetPhone(int id)
        {
            var PhoneData = _context.ProductDetails.SingleOrDefault(p=> p.Id == id);
            if (PhoneData == null)
            {
                return new JsonResult("Not found");
            }
            return new JsonResult(Ok(PhoneData));
        }
        [HttpDelete("DeletePhone/{id}")]
        public JsonResult DeletePhone(int id)
        {
            var productDetails = _context.ProductDetails.SingleOrDefault(p => p.Id == id);
            if (productDetails != null)
            {
                _context.ProductDetails.Remove(productDetails);
                _context.SaveChanges();
                return new JsonResult(Ok("Deleted"));
            }
            return new JsonResult("Not found");
        }
        [HttpPut("AddNewPhone")]
        public JsonResult AddNewPhone(ProductDetails product)
        {
            var PhoneData = _context.ProductDetails.Add(product);
            if (PhoneData == null)
            {
                return new JsonResult("couldn't add");
            }
            return new JsonResult(Ok(PhoneData));
        }

        [HttpGet("Cat")]
        public async Task<JsonResult> GetCatData() { 
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "https://catfact.ninja/fact");
            var response = await client.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();

            return new JsonResult(content);
        }
    }
}
