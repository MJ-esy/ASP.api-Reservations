using ASP_Reservations.DTO;
using ASP_Reservations.Models;
using ASP_Reservations.Repositories.IRepositories;
using ASP_Reservations.Services.IServices;

namespace ASP_Reservations.Services
{
    public class UserServices : IUserServices
    {
        //Create private readonly field for Repositories
        private readonly IUserRepo _userRepo;
        private readonly IBookingRepo _bookingRepo;
        //Create a constructor to inject Repositories
        public UserServices(IUserRepo userRepo, IBookingRepo bookingRepo)
        {
            _userRepo = userRepo;
            _bookingRepo = bookingRepo;
        }

        public async Task<bool> CreateUserAsync(CreateUserDTO userDTO)
        {
            var newUser = new User
            {
                Name = userDTO.Name,
                Email = userDTO.Email,
                Phone = userDTO.Phone
            };
            var newUserId = await _userRepo.CreateUserAsync(newUser);
            if (newUserId != null)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _userRepo.GetUserByIdAsync(id);
            if (user == null)
            {
                return false;
            }
            var rowsAffected = await _userRepo.DeleteUserAsync(id);
            if (rowsAffected > 0)
            {
                return true;
            }
            return false;
        }

        public async Task<List<UserDTO>> GetAllUsersAsync()
        {
            var users = await _userRepo.GetAllUsersAsync();
            var userDTOs = users.Select(u => new UserDTO
            {
                Name = u.Name,
                Email = u.Email,
                Phone = u.Phone
            }).ToList();
            return userDTOs;
        }

        public async Task<UserDTO> GetUserAndBookingCountAsync(int id)
        {
            var user = await _userRepo.GetUserByIdAsync(id);
            if (user == null)
            {
                return null;
            }
            var bookingCount = await _bookingRepo.GetBookingsByUserIdAsync(id);
            var totalBookings = bookingCount.Count;
            var userDTO = new UserDTO
            {
                Name = user.Name,
                Email = user.Email,
                Phone = user.Phone,
                TotalBookings = totalBookings
            };
            return userDTO;

        }

        public async Task<UserDTO> GetUserByIdAsync(int id)
        {
            var user = await _userRepo.GetUserByIdAsync(id);
            if (user == null)
            {
                return null;
            }
            var userDTO = new UserDTO
            {
                Name = user.Name,
                Email = user.Email,
                Phone = user.Phone
            };
            return userDTO;
        }

        public async Task<bool> UpdateUserAsync(int id, UpdateUserDTO userDTO)
        {
            var user = await _userRepo.GetUserByIdAsync(id);
            if (user == null)
            {
                return false;
            }

            var updatedUser = new User
            {
                UserId = id,
                Name = userDTO.Name,
                Email = userDTO.Email,
                Phone = userDTO.Phone
            };
            var result = await _userRepo.UpdateUserAsync(updatedUser);
            if (result != null)
            {
                return true;
            }
            return false;

        }
    }
}
