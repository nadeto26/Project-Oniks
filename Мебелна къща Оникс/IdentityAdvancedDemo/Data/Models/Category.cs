using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace IdentityAdvancedDemo.Data.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        public ICollection<Furnitures> Furnitures { get; set; } = new List<Furnitures>();

        public ICollection<Discount> Discounts { get; set; } = new List<Discount>();
    }
}