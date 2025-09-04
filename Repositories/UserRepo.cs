using ASP_Reservations.Data;
using ASP_Reservations.Models;
using ASP_Reservations.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace ASP_Reservations.Repositories
{
    public class UserRepo : IUserRepo
    {
        //create a private readonly field for AppDbContext
        private readonly AppDbContext _context;

        //create a constructor to inject AppDbContext
        public UserRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user.UserId; // Return the UserId of the created user
        }

        public async Task<int> DeleteUserAsync(int userId)
        {
            var rowsAffected = await _context.Users.Where(u => u.UserId == userId).ExecuteDeleteAsync();
            return rowsAffected; // Return the number of rows affected
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            var users = await _context.Users.ToListAsync();
            return users; // Return the list of users
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            var userById = await _context.Users.FirstOrDefaultAsync(u => u.UserId == id);
            return userById;
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return user; // Return the updated user object
        }
    }
}
