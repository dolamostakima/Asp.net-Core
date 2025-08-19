using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyProject.Models
{
    public class FruitsSalescs
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Fruits")]
        public int FruitsId { get; set; }
        [Display(Name = "Sales  Amount")]
        public decimal SalesAmount { get; set; }
        public int UOM { get; set; } // Unit of Measure
        public virtual Fruits Fruits { get; set; } 

    }
}
