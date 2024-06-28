namespace IdentityAdvancedDemo.Models.Furnitures
{
    public class FurnitureQuaryModel
    {
        public FurnitureQuaryModel()
        {
            Furnitures = new List<AllFurnitureViewModel>();
        }
        public int ToTalFurnitureCount { get; set; } 

        public IEnumerable<AllFurnitureViewModel> Furnitures { get; set; }
    }
}
