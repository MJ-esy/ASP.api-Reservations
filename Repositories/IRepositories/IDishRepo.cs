using ASP_Reservations.Models;

namespace ASP_Reservations.Repositories.IRepositories
{
    public interface IDishRepo
    {
        Task<List<Dish>> GetAllDishesAsync();
        Task<Dish> GetDishByIdAsync(int id);
        Task<Dish> CreateDishAsync(Dish dish);
        Task<bool> UpdateDishAsync(Dish dish);
        Task<bool> DeleteDishAsync(int id);
    }
}
