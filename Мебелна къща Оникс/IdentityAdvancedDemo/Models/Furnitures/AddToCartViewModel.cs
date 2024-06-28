namespace IdentityAdvancedDemo.Models.Furnitures
{
    public class AddToCartViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string ImageUrl { get; set; } = null!;

        public decimal? Price { get; set; }

        public decimal? NewPrice { get; set; }

        public decimal? OldPrice { get; set; }

        public string Category { get; set; } = null!;

        public int Quenitity { get; set; } = 1;

       
    }
}
