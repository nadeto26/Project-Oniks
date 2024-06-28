using IdentityAdvancedDemo.Data.IdentityModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdentityAdvancedDemo.Data.Models
{
    public class AccessoryBuyer
    {
        public Guid BuyerId { get; set; } // Променете типа на BuyerId на Guid

        [ForeignKey(nameof(BuyerId))]
        public ApplicationUser Buyer { get; set; } = null!;

        public int AccesoaryId { get; set; }

        [ForeignKey(nameof(AccesoaryId))]
        public Accessories Accessories { get; set; } = null!;

        public int Quentity { get; set; } = 1;
    }
}