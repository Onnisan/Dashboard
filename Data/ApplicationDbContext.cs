using Dashboard.Models;
using Microsoft.EntityFrameworkCore;

namespace Dashboard.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductDetails> ProductDetails { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Invoice>  Invoices { get; set; }

        public DbSet<PaymentAccept> PaymentAccepts { get; set; }

        public DbSet<Cart> Carts { get; set; }
    }
}
