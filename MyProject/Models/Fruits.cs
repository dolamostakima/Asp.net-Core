using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyProject.Models
{
    public class Fruits
    {
        [Key]
        public int Id { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
        [NotMapped]
        public IFormFile Image { get; set; }

        public string ImagePath { get; set; } = string.Empty;
        [Range(1, 10000, ErrorMessage = "Price must be between 0 and 10000")]
        public decimal Price { get; set; }

        [Display(Name = "Available")]
        public bool IsAvailable { get; set; } = true;

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}" , ApplyFormatInEditMode = true)]
        [Display(Name = "Buy Date")]
        public DateTime BuyDate { get; set; } = DateTime.Now;

        public virtual IList<FruitsSalescs> FruitsSales { get; set; } = new List<FruitsSalescs>();

    }
}
