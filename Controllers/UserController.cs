using ASP_Reservations.DTO;
using ASP_Reservations.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASP_Reservations.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        //Create reference to IUserservices
        private readonly IUserServices _userServices;

        public UserController(IUserServices userServices)
        {
            _userServices = userServices;
        }

        [HttpGet("allUsers")]
        [Authorize]
        public async Task<ActionResult<List<UserDTO>>> GetAllUsers()
        {
            var users = await _userServices.GetAllUsersAsync();
            return Ok(users);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUserById(int id)
        {
            var user = await _userServices.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound(new { message = "User not found." });
            }
            return Ok(user);

        }

        [HttpGet("BookingCount/{id}")]
        public async Task<ActionResult<UserDTO>> GetUserAndBookingCount(int id)
        {
            var userWithBookingCount = await _userServices.GetUserAndBookingCountAsync(id);
            if (userWithBookingCount == null)
            {
                return NotFound(new { message = "User not found." });
            }
            return Ok(userWithBookingCount);
        }

        [HttpPost("create")]
        public async Task<ActionResult> CreateUser([FromBody] CreateUserDTO userDTO)
        {
            var result = await _userServices.CreateUserAsync(userDTO);
            if (result)
            {
                return Ok(new { message = "User created successfully!" });
            }
            return BadRequest(new { message = "Failed to create user." });
        }

        [HttpPut("update/{id}")]
        public async Task<ActionResult> UpdateUser(int id, [FromBody] UpdateUserDTO userDTO)
        {
            var result = await _userServices.UpdateUserAsync(id, userDTO);
            if (result)
            {
                return Ok(new { message = "User updated successfully!" });
            }
            return BadRequest(new { message = "Failed to update user." });
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            var result = await _userServices.DeleteUserAsync(id);
            if (result)
            {
                return Ok(new { message = "User deleted successfully!" });
            }
            return BadRequest(new { message = "Failed to delete user." });
        }

    }
}
