using System.ComponentModel.DataAnnotations;

namespace IdentityAdvancedDemo.Models.Disscounts
{
    public class AllDiscountViewModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        [MinLength(10)]
        public string Name { get; set; } = null!;

        [Required]
        public string ImageUrl { get; set; } = null!;

        public decimal OldPrice { get; set; }

        public decimal NewPrice { get; set; }

        public int? Height { get; set; }

        public int? Weight { get; set; }

        public int Delivery { get; set; }

        [Required]
        [MinLength(10)]
        [MaxLength(100)]
        public string Importer { get; set; } = null!;
    }
}
