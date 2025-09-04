using ASP_Reservations.DTO;
using ASP_Reservations.Models;

namespace ASP_Reservations.Services.IServices
{
    public interface IUserServices
    {
        Task<List<UserDTO>> GetAllUsersAsync();
        Task<UserDTO> GetUserByIdAsync(int id);
        Task<bool> CreateUserAsync(CreateUserDTO userDTO); //To return true/false
        Task<bool> UpdateUserAsync(int id, UpdateUserDTO userDTO); //To return true/false
        Task<bool> DeleteUserAsync(int id); //To return true/false
        Task<UserDTO> GetUserAndBookingCountAsync(int id); //To return UserDTO with booking count
    }
}
