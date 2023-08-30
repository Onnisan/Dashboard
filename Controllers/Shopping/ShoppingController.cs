﻿using Dashboard.Data;
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
    }
}
