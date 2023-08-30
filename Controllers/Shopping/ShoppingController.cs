using Dashboard.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dashboard.Controllers.Shopping
{
    public class ShoppingController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ShoppingController(ApplicationDbContext context)
        {
            this._context = context;
        }

        public IActionResult Index()
        {
            var product = _context.Products.ToList();

            return View(product);
        }

        public IActionResult ProductDetails(int id)
        {
            var productDetails = _context.ProductDetails.Where(p => p.ProductId == id).ToList();
            return View(productDetails);
        }
        [Authorize]
        public IActionResult Checkout(int id)
        {
            var user = HttpContext.User.Identity.Name;
            // var productDetails = _context.ProductDetails.Where(p => p.ProductId == id).ToList();
            return View();
        }
    }
}
