using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyProject.Data;
using MyProject.Models;

namespace MyProject.Controllers
{
    public class FruitsController : Controller
    {
        private readonly ApplicationDbContext _context;
        
        private readonly IWebHostEnvironment _host;

        public FruitsController(ApplicationDbContext context , IWebHostEnvironment host)
        {
            _context = context;
            _host = host;
        }
        
        // GET: Fruits
        public async Task<IActionResult> Index()
        {
            return View(await _context.Fruits.ToListAsync());
        }

        // GET: Fruits/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fruits = await _context.Fruits.Include(s=>s.FruitsSales)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fruits == null)
            {
                return NotFound();
            }

            return View(fruits);
        }

       [Authorize]
        // GET: Fruits/Create
        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Add_fruitSale()
        {
            var fruit = new Fruits()
            {
                Id = 0,
                Name = string.Empty, ImagePath = string.Empty,
                Price = 0, IsAvailable = true,     BuyDate = DateTime.Now
            };
            fruit.FruitsSales.Add(new FruitsSalescs()
            {
                Id = 0,  FruitsId = 0,   SalesAmount = 0,    UOM = 0
            });
            
                return View("add_fruitsales", fruit);
        }

        // POST: Fruits/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
       // [Authorize]
        public async Task<IActionResult> Create(Fruits fruits)
        {
               string wwwrootPath = _host.WebRootPath+ "/upload/"+ fruits.Image.FileName;
               string storePaht = "upload/" + fruits.Image.FileName;

                using (var stream = new FileStream(wwwrootPath, FileMode.Create))
                {
                    await fruits.Image.CopyToAsync(stream);
                }

                fruits.ImagePath = storePaht;

                _context.Add(fruits);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
          
            return View(fruits);
        }
       [Authorize]
        // GET: Fruits/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

           

            var fruits = await _context.Fruits.Include(fs => fs.FruitsSales).Where(w => w.Id == id).FirstOrDefaultAsync();

          

            if (fruits == null)
            {
                return NotFound();
            }
            return View(fruits);
        }

        // POST: Fruits/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Fruits fruits)
        {

            string wwwrootPath = _host.WebRootPath + "/upload/" + fruits.Image.FileName;
            string storePaht = "upload/" + fruits.Image.FileName;

            using (var stream = new FileStream(wwwrootPath, FileMode.Create))
            {
                await fruits.Image.CopyToAsync(stream);
            }

            fruits.ImagePath = storePaht;


            if (id != fruits.Id)
            {
                return NotFound();
            }

            foreach (var fruitSale in fruits.FruitsSales)
            {
                if (fruitSale.Id == 0)
                {
                    fruitSale.FruitsId = fruits.Id; // Ensure the FruitsId is set for new sales entries
                    _context.Add(fruitSale);
                }
                else
                {
                  var da = fruits.FruitsSales.Where(w => w.Id != 0).Select(s => s.Id).ToList();
                  var orda = _context.FruitsSalescs.Where(w => w.FruitsId == fruits.Id).Select(s => s.Id);
                 var abc = orda.Where(w => !da.Contains(w)).ToList();
                  foreach (var item in abc)
                  {
                      var fruitSaleToRemove = _context.FruitsSalescs.Find(item);
                      if (fruitSaleToRemove != null)
                      {
                          _context.FruitsSalescs.Remove(fruitSaleToRemove);
                      }
                    }


                }

                await _context.SaveChangesAsync();
            }

                try
                {
                    _context.Update(fruits);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FruitsExists(fruits.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
         
            return View(fruits);
        }

      [Authorize]
        // GET: Fruits/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fruits = await _context.Fruits.Include(fs => fs.FruitsSales)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fruits == null)
            {
                return NotFound();
            }

            return View(fruits);
        }

        // POST: Fruits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fruits = await _context.Fruits.FindAsync(id);
            if (fruits != null)
            {
                _context.FruitsSalescs.RemoveRange(fruits.FruitsSales.ToArray());
                _context.Fruits.Remove(fruits);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FruitsExists(int id)
        {
            return _context.Fruits.Any(e => e.Id == id);
        }
    }
}
