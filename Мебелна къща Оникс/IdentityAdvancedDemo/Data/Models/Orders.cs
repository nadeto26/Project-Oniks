using IdentityAdvancedDemo.Data.IdentityModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdentityAdvancedDemo.Data.Models
{
    public class Orders
    {
        [Key]
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

        public Guid BuyerId { get; set; }

        [ForeignKey(nameof(BuyerId))]
        public ApplicationUser Buyer { get; set; } = null!;

        public int? DiscountId { get; set; }

        [ForeignKey(nameof(DiscountId))]
        public Discount? Discounts { get; set; }

        public string? DiscountName { get; set; }

        public int QuentityDiscount { get; set; } = 1;

        public int? FurnitureId { get; set; }

        [ForeignKey(nameof(FurnitureId))]
        public Furnitures? Furnitures { get; set; }

        public string? FurnitureName { get; set; }

        public int QuentityFurniture { get; set; } = 1;

        public int? AccessoryId { get; set; }

        [ForeignKey(nameof(AccessoryId))]
        public Accessories? Accessories { get; set; }

        public string? AccessoriesName { get; set; }

        public int QuentityAccessory { get; set; } = 1;
    }
}
