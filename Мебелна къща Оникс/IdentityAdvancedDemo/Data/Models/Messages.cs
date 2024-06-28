using System.ComponentModel.DataAnnotations;

namespace IdentityAdvancedDemo.Data.Models
{
    public class Messages
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;

        public string About { get; set;} = null!;

        public string Message { get; set; } = null!;

       


    }
}
