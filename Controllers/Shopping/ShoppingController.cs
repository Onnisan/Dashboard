using Dashboard.Data;
using Dashboard.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MailKit.Net.Smtp;

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
        public async Task<string> SendMail() {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Test Email", "onnisky@gmail.com")); //sender
            message.To.Add(MailboxAddress.Parse("abdullahplanet@yahoo.com")); // reciever
            message.Subject="Test message from my apps";
            message.Body = new TextPart("plain")
            {
                Text = "<h1>This is a test message from my App</h1>"
            };


            using (var client = new SmtpClient())
            {   
                try
                {
                    client.Connect("smtp.gmail.com", 587);
                    client.Authenticate("onnisky@gmail.com", "svfbanzyijbqivet");
                    await client.SendAsync(message);

                }catch (Exception e){
                    Console.WriteLine(e.Message.ToString());
                    client.Disconnect(true);
                }
            }
            return "ok";
        }


        [Authorize]
        public IActionResult Checkout(int id)
        {
            var user = HttpContext.User.Identity.Name;
            var productDetails = _context.ProductDetails.SingleOrDefault(p => p.Id == id);
            var cart = new Cart(){
                IdCostumer = user,
                MyProductId = productDetails.ProductId,
                Color = productDetails.Color,
                Images = productDetails.Image,
                Price = productDetails.Price,
                Total = (productDetails.Price * 0.15) + productDetails.Price,
                ProductName = productDetails.Descreption,
                Tax = 0.15
            };

            _context.Carts.Add(cart);
            _context.SaveChanges();

            return View(cart);
        }

        [Authorize]
        public async Task<IActionResult> Invoice(int id)
        {
            var cart = _context.Carts.SingleOrDefault(p => p.Id == id);
            var invoice = new Invoice()
            {
                IdCostumer = cart.IdCostumer,
                ProductId = cart.MyProductId,
                ProductColor = cart.Color,
                ProductImages = cart.Images,
                Price = cart.Price,
                Total = cart.Total,
                NameProduct = cart.ProductName,
                Tax = cart.Tax,
                Discount = 0,
                QTY = 1,
            };

            _context.Invoices.Add(invoice);
            _context.SaveChanges();

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Invoice order No."+invoice.Id, "onnisky@gmail.com")); //sender
            message.To.Add(MailboxAddress.Parse(invoice.IdCostumer.ToString())); // reciever
            message.Subject = "Invoice order No." + invoice.Id;
            message.Body = new TextPart("plain")
            {
                Text = "<h1>Here is a link for your </h1><a href='abdullahdashboardtuwaiq.azurewebsites.net/shopping/invoice/" + invoice.Id+"'>invoice</a>"
            };


            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect("smtp.gmail.com", 587);
                    client.Authenticate("onnisky@gmail.com", "svfbanzyijbqivet");
                    await client.SendAsync(message);

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message.ToString());
                    client.Disconnect(true);
                }
            }

            return View(invoice);
        }
    }
}
