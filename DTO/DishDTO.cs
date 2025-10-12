using System.ComponentModel.DataAnnotations;

namespace ASP_Reservations.DTO
{
  public class DishDTO
  {
    public string DishName { get; set; }
    public decimal Price { get; set; }
    public string? Description { get; set; }
    public bool IsPopular { get; set; }
    public string Category { get; set; }
    public string Allergen { get; set; }
    public string? ImageUrl { get; set; }

  }

  public class CreateDishDTO
  {
    [Required(ErrorMessage = "Dish Name is required")]
    [StringLength(60, ErrorMessage = "Dish Name cannot exceed 60 characters")]
    public string DishName { get; set; } = string.Empty;
    [Required(ErrorMessage = "Price is required")]
    [Range(0.01, 1000.00, ErrorMessage = "Price must be between 0.01 and 1000.00")]
    public decimal Price { get; set; }
    [StringLength(300, ErrorMessage = "Description cannot exceed 300 characters")]
    public string? Description { get; set; }
    public bool IsPopular { get; set; } = false;
    [Required(ErrorMessage = "Category is required")]
    [StringLength(50, ErrorMessage = "Category cannot exceed 50 characters")]
    public string Category { get; set; } = string.Empty;
    [Required(ErrorMessage = "Allergen information is required")]
    public string Allergen { get; set; } = string.Empty;
    [Url(ErrorMessage = "Invalid URL format")]
    public string? ImageUrl { get; set; }
  }

  public class UpdateDishDTO
  {

    [Required(ErrorMessage = "Dish Name is required")]
    [StringLength(60, ErrorMessage = "Dish Name cannot exceed 60 characters")]
    public string DishName { get; set; } = string.Empty;
    [Required(ErrorMessage = "Price is required")]
    [Range(0.01, 1000.00, ErrorMessage = "Price must be between 0.01 and 1000.00")]
    public decimal Price { get; set; }
    [StringLength(300, ErrorMessage = "Description cannot exceed 300 characters")]
    public string? Description { get; set; }
    public bool IsPopular { get; set; } = false;
    [Required(ErrorMessage = "Category is required")]
    [StringLength(50, ErrorMessage = "Category cannot exceed 50 characters")]
    public string Category { get; set; } = string.Empty;
    [Required(ErrorMessage = "Allergen information is required")]
    public string Allergen { get; set; } = string.Empty;
    [Url(ErrorMessage = "Invalid URL format")]
    public string? ImageUrl { get; set; }
  }

  public class DeleteDishDTO
  {
    [Required]
    public int DishId { get; set; }
  }


}
