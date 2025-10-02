using ASP_Reservations.DTO;
using ASP_Reservations.Models;
using ASP_Reservations.Models.Enums;
using ASP_Reservations.Repositories.IRepositories;
using ASP_Reservations.Services.IServices;

namespace ASP_Reservations.Services
{
    public class DishServices : IDishServices
    {

        private readonly IDishRepo _dishRepo;
        public DishServices(IDishRepo dishRepo)
        {
            _dishRepo = dishRepo;
        }
        public async Task<bool> CreateDishAsync(CreateDishDTO dishDTO)
        {
            try
            {
                var newDish = new Dish
                {
                    DishName = dishDTO.DishName,
                    Price = dishDTO.Price,
                    Description = dishDTO.Description,
                    IsPopular = dishDTO.IsPopular,
                    Category = Enum.Parse<DishCategories>(dishDTO.Category),
                    Allergen = Enum.Parse<Allergy>(dishDTO.Allergen),
                    ImageUrl = dishDTO.ImageUrl
                };

                // Save the new dish to the repository
                var createResult = await _dishRepo.CreateDishAsync(newDish);

                // If the dish was successfully created, return true
                return createResult != null;
            }
            catch (Exception ex)
            {
                // Log the exception (optional)
                Console.WriteLine($"Error creating dish: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteDishAsync(int id)
        {
            var dish = await _dishRepo.GetDishByIdAsync(id);
            if (dish == null)
            {
                throw new Exception("Dish not found.");
            }
            var deleteResult = await _dishRepo.DeleteDishAsync(id);
            if (deleteResult) return true;
            return false;

        }

        public async Task<List<DishDTO>> GetAllDishesAsync()
        {
            var dishes = await _dishRepo.GetAllDishesAsync();
            return dishes.Select(d => new DishDTO
            {
                DishName = d.DishName,
                Price = d.Price,
                Description = d.Description,
                IsPopular = d.IsPopular,
                Category = d.Category.ToString(),
                Allergen = string.Join(", ", Enum.GetValues(typeof(Allergy))
            .Cast<Allergy>()
            .Where(a => d.Allergen.HasFlag(a) && a != Allergy.None)),
                ImageUrl = d.ImageUrl
            }).ToList();

        }

        public async Task<DishDTO> GetDishByIdAsync(int id)
        {
            var dish = await _dishRepo.GetDishByIdAsync(id);
            if (dish == null)
            {
                throw new Exception("Dish not found.");
            }
            return new DishDTO
            {
                DishName = dish.DishName,
                Price = dish.Price,
                Description = dish.Description,
                IsPopular = dish.IsPopular,
                Category = dish.Category.ToString(),
                Allergen = string.Join(", ", Enum.GetValues(typeof(Allergy))
            .Cast<Allergy>()
            .Where(a => dish.Allergen.HasFlag(a) && a != Allergy.None)),
                ImageUrl = dish.ImageUrl
            };
        }

        public async Task<bool> UpdateDishAsync(int id, UpdateDishDTO dishDTO)
        {
            var existingDish = await _dishRepo.GetDishByIdAsync(id);
            if (existingDish == null)
            {
                throw new Exception("Dish not found.");
            }
            existingDish.DishId = id;
            existingDish.DishName = dishDTO.DishName;
            existingDish.Price = dishDTO.Price;
            existingDish.Description = dishDTO.Description;
            existingDish.IsPopular = dishDTO.IsPopular;
            existingDish.Category = Enum.Parse<DishCategories>(dishDTO.Category);
            existingDish.Allergen = Enum.Parse<Allergy>(dishDTO.Allergen);
            existingDish.ImageUrl = dishDTO.ImageUrl;
            var updateSucceeded = await _dishRepo.UpdateDishAsync(existingDish);
            if (updateSucceeded)
            {
                Console.WriteLine("Dish updated successfully!");
                return true;
            }
            return false;
        }
    }
}
