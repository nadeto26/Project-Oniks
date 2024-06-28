using System.ComponentModel.DataAnnotations;

namespace IdentityAdvancedDemo.Data.Models
{
    public class Discount
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;

        [Required]
        public string ImageUrl { get; set; } = null!;

        public decimal NewPrice { get; set; }

        public decimal OldPrice { get; set; }

        public int? CategoryId { get; set; }

        public Category? Category { get; set; } = null!;

        public int? Height { get; set; }

        public int? Weight { get; set; }

        public int Delivery { get; set; }

        [Required]
        [MaxLength(100)]
        public string Importer { get; set; } = null!;

        public ICollection<DiscountBuyer> DiscountBuyers
          = new HashSet<DiscountBuyer>();
    }
}
