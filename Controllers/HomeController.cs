using Dashboard.Data;
using Dashboard.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Dashboard.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            this._context = context;
        }

        [Authorize]
        public IActionResult Index()
        {
            var product = _context.Products.ToList();
            return View(product);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult addProduct(Product product) { 
            _context.Products.Add(product);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id) {
            var product = _context.Products.SingleOrDefault(p => p.Id == id);
            if (product != null) { 
            _context.Products.Remove(product);
            _context.SaveChanges();
            }
            return RedirectToAction("Index");   
        }
        [HttpPost]
		public IActionResult ProductDetails(int id)
		{
			var products = _context.Products.ToList();
			var productDetails = _context.ProductDetails.Where(p => p.ProductId == id).ToList();
			ViewBag.ProductDetails = productDetails;
			return View(products);
		}
		public IActionResult ProductDetails()
        {   
            var products = _context.Products.ToList();
            var productDetails = _context.ProductDetails.ToList();
            ViewBag.ProductDetails = productDetails;
            return View(products);
        }

        public IActionResult AddProductDetails(ProductDetails productDetails)
        {
            _context.ProductDetails.Add(productDetails);
            _context.SaveChanges();
            return RedirectToAction("ProductDetails");
        }

        public IActionResult Edit(int id)
        {
            var product = _context.Products.SingleOrDefault(p=> p.Id == id);
            return View(product);   
        }

        public IActionResult UpdateProduct(Product product)
        {
            _context.Products.Update(product);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}