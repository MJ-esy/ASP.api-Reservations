using ASP_Reservations.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASP_Reservations.Models
{
    public class Dish
    {
        [Key]
        public int DishId { get; set; }

        [MaxLength(60)]
        public string DishName { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [MaxLength(300)]
        public string? Description { get; set; }

        public bool IsPopular { get; set; }

        [MaxLength(50)]
        [Required]
        public DishCategories Category { get; set; }

        [Required]
        public Allergy Allergen { get; set; }

        [Url]
        public string? ImageUrl { get; set; }
    }
}
