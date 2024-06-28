namespace IdentityAdvancedDemo.Models.Messages
{
    public class MessageViewModel
    {
       
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;

        public string About { get; set; } = null!;

        public string Message { get; set; } = null!;
    }
}
