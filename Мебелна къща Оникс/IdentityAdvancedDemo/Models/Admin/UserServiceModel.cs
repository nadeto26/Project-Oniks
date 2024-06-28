using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace IdentityAdvancedDemo.Models.Admin
{
    public class UserServiceModel
    {
        public string Email { get; set; } = null!;

        [Display(Name = "Full Name")]
        public string FullName { get; set; } = null!;
    }
}
