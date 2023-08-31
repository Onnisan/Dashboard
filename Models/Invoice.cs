using System.ComponentModel.DataAnnotations;

namespace Dashboard.Models
{
    public class Invoice
    {
        [Key]
        public int Id { get; set; }

        public string IdCostumer { get; set; }

        public int ProductId { get; set; }

        public string NameProduct { get; set; }

        public double Price { get; set; }

        public string ProductColor { get; set; }

        public string ProductImages { get; set; }

        public int QTY { get; set; }

        public double Tax { get; set; }

        public float Discount { get; set; }

        public double Total { get; set; }

    }
}
