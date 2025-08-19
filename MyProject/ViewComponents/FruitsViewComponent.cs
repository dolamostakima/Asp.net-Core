using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyProject.Data;

namespace MyProject.ViewComponents
{
    public class FruitsViewComponent : ViewComponent
    {
        ApplicationDbContext _context;
        public FruitsViewComponent(ApplicationDbContext context)
        {
            // Constructor logic if needed
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var fruits = await _context.Fruits.Include(fs => fs.FruitsSales).ToListAsync();
            var fvm = new ViewModel.FruitViewModel
            {
                Fruits = fruits
            };
            if (fvm.Fruits.Count() == 0)
            { 
                fvm.Fruits.Add(new Models.Fruits
                {
                    Id = 0 ,
                    Name = "No Fruits Found",
                    ImagePath = "/images/no-fruits.png",
                    Price = 0,
                    IsAvailable = false,
                    BuyDate = DateTime.Now
                });
            }
            return View(fvm);
        }
    }
    
}
