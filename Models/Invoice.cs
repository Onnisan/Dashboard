using System.ComponentModel.DataAnnotations;

namespace Dashboard.Models
{
    public class Invoice
    {
        [Key]
        public int Id { get; set; }

        public int CostumerId { get; set; }

        public int ProductId { get; set; }

        public double Price { get; set; }

        public int QTY { get; set; }

        public float Tax { get; set; }

        public float Discount { get; set; }

        public double Total { get; set; }
    }
}
