using MyProject.Models;

namespace MyProject.ViewModel
{
    public class FruitViewModel
    {
        public List<Fruits> Fruits { get; set; } = new List<Fruits>();
        public List<FruitsSalescs> FruitsSales { get; set; } = new List<FruitsSalescs>();
    }
}
