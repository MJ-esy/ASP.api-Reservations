using ASP_Reservations.Models;

namespace ASP_Reservations.Repositories.IRepositories
{
    public interface IUserRepo
    {
        Task<List<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(int id);
        Task<int> CreateUserAsync(User user); //To return UserId of created user
        Task<User> UpdateUserAsync(User user); //To return updated user object
        Task<int> DeleteUserAsync(int id); //To return number of rows affected
    }
}
