using IdentityAdvancedDemo.Data.IdentityModels;
using IdentityAdvancedDemo.Migrations;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdentityAdvancedDemo.Data.Models
{
    public class FurnitureBuier
    {
        public Guid BuyerId { get; set; } 

        [ForeignKey(nameof(BuyerId))]
        public ApplicationUser Buyer { get; set; } = null!;

        public int FurnitureId { get; set; }

        [ForeignKey(nameof(FurnitureId))]
        public Furnitures Furnitures { get; set; } = null!;

        public int Quentity { get; set; } = 1;
    }
}
