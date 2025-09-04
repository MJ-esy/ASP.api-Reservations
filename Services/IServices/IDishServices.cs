using ASP_Reservations.DTO;

namespace ASP_Reservations.Services.IServices
{
    public interface IDishServices
    {
        Task<List<DishDTO>> GetAllDishesAsync();
        Task<DishDTO> GetDishByIdAsync(int id);
        Task<bool> CreateDishAsync(CreateDishDTO dishDTO);
        Task<bool> UpdateDishAsync(int id, UpdateDishDTO dishDTO);
        Task<bool> DeleteDishAsync(int id);
    }
}
