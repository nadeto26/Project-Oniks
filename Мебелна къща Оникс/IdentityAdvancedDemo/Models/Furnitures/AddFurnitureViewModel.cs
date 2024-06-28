using IdentityAdvancedDemo.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace IdentityAdvancedDemo.Models.Furnitures
{
    public class AddFurnitureViewModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(1000)]
        public string Description { get; set; } = null!;

        public decimal? Price { get; set; }

        [Required]
        public string ImageUrl { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        [MinLength(10)]
        public string Importer { get; set; } = null!;

        public int Delivery { get; set; }

        public decimal? NewPrice { get; set; }

        public decimal? OldPrice { get; set; }

        public int? Height { get; set; }

        public int? Weight { get; set; }

        public int CategoryId { get; set; }

        [Required]
        public string Category { get; set; } = null!;

        [Required]
        public IEnumerable<Category> Categories { get; set; } = new List<Category>();
    }
}
