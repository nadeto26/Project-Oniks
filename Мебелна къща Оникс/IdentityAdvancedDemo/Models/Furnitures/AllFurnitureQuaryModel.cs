using System.ComponentModel.DataAnnotations;

namespace IdentityAdvancedDemo.Models.Furnitures
{
    public class AllFurnitureQuaryModel
    {
        public const int FurniturePerPage = 3;

        public string Category { get; set; }

        [Display(Name = "Търси по текст")]
        public string SearchItem { get; set; } = null!;

        public FurnitureSort Sorting { get; set; }

        public int CurrentPage { get; set; } = 1;

        public int TotalFurnitureCount { get; set; }

        public IEnumerable<string> Categories { get; set; } = null!;

        public IEnumerable<AllFurnitureViewModel> Furnitures { get; set;}
        = new List<AllFurnitureViewModel>();
    }
}
