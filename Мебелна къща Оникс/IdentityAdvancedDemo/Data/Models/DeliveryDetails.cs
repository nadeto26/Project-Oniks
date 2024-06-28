using System.ComponentModel.DataAnnotations;

namespace IdentityAdvancedDemo.Data.Models
{
    public class DeliveryDetails
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; } = null!;

        [Required]
        public string Address { get; set; } = null!;

        [Required]
        public string City { get; set; } = null!;

        [Required]
        public string PostCode { get; set; } = null!;
        public string Email { get; internal set; }
        public string PhoneNumber { get; internal set; }
    }
}
