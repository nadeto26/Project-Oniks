using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace IdentityAdvancedDemo.Models.Admin
{
    public class Oders
    {
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; } = null!;

        [Required]
        public string Address { get; set; } = null!;

        [Required]
        public string City { get; set; } = null!;

        public string Phonenumber { get; set; } = null!;

        [Required]
        public string PostCode { get; set; } = null!;

        public string? DiscountName { get; set; }

        public int QuentityDiscount { get; set; } = 1;

        public int? FurnitureId { get; set; }


        public string? FurnitureName { get; set; }

        public int QuentityFurniture { get; set; } = 1;

        public int? AccessoryId { get; set; }


        public string? AccessoriesName { get; set; }

        public int QuentityAccessory { get; set; } = 1;
    }
}
