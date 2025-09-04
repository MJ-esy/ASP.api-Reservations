using ASP_Reservations.Data;
using ASP_Reservations.Models;
using ASP_Reservations.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace ASP_Reservations.Repositories
{
    public class DishRepo : IDishRepo
    {
        private readonly AppDbContext _context;
        public DishRepo(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Dish> CreateDishAsync(Dish dish)
        {
            var newDish = _context.Dishes.Add(dish);
            await _context.SaveChangesAsync();
            return newDish.Entity;
        }

        public async Task<bool> DeleteDishAsync(int id)
        {
            var rowsAffected = await _context.Dishes.Where(d => d.DishId == id)
                .ExecuteDeleteAsync();
            if (rowsAffected > 0)
            {
                return true;
            }
            return false;

        }

        public async Task<List<Dish>> GetAllDishesAsync()
        {
            var dishes = await _context.Dishes.ToListAsync();
            return dishes; // Return the list of dishes
        }

        public async Task<Dish> GetDishByIdAsync(int id)
        {
            var dish = await _context.Dishes.FirstOrDefaultAsync(d => d.DishId == id);
            return dish;
        }

        public async Task<bool> UpdateDishAsync(Dish dish)
        {
            var updatedDish = _context.Dishes.Update(dish);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return true;
            }
            return false;
        }
    }
}
