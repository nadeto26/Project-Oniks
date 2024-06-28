using IdentityAdvancedDemo.Data.IdentityModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdentityAdvancedDemo.Data.Models
{
    public class DiscountBuyer
    {
        public Guid BuyerId { get; set; } // Променете типа на BuyerId на Guid

        [ForeignKey(nameof(BuyerId))]
        public ApplicationUser Buyer { get; set; } = null!;

        public int DiscountId { get; set; }

        [ForeignKey(nameof(DiscountId))]
        public Discount Discounts { get; set; } = null!;

        public int Quentity { get; set; } = 1;
    }
}