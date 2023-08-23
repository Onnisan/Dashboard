using System.ComponentModel.DataAnnotations;

namespace Dashboard.Models
{
    public class ProductDetails
    {
        [Key]
        public int Id { get; set; }

        public int ProductId { get; set; }
        public string Descreption { get; set; }

        public double Price { get; set; }
		public string Qty { get; set; }
		public string Model { get; set; }
        public string Color { get; set; }
        public string Image { get; set; }
		public string Notes { get; set; }
	}
}
