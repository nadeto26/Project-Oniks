using System.ComponentModel.DataAnnotations;

namespace IdentityAdvancedDemo.Models.Accessories
{
    public class AllViewModelAccesoaries
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        [MinLength(5)]
        public string Name { get; set; } = null!;

        public decimal? Price { get; set; }

        public int Delivery { get; set; }

        [Required]
        public string ImageUrl { get; set; } = null!;

        public decimal? NewPrice { get; set; }

        public decimal? OldPrice { get; set; }

        [Required]
        [MaxLength(500)]
        public string Description { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        public string Importer { get; set; } = null!;
    }

}
