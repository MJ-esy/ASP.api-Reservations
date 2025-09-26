using ASP_Reservations.Data;
using ASP_Reservations.DTO;
using ASP_Reservations.Models;
using ASP_Reservations.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASP_Reservations.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly TokenServices _tokenServices;
        public AuthController(AppDbContext context, TokenServices tokenServices)
        {
            _context = context;
            _tokenServices = tokenServices;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register(AdminDTO newAdmin)
        {
            var existingAdmin = await _context.Admins.FirstOrDefaultAsync(a => a.Username == newAdmin.Username);
            if (existingAdmin != null)
            {
                return BadRequest("Username already exists.");
            }

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(newAdmin.Password);
            var admin = new Admin
            {
                Username = newAdmin.Username,
                PasswordHash = passwordHash,
                Role = "Admin"
            };
            _context.Admins.Add(admin);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginAdminDTO loginAdmin)
        {
            var admin = await _context.Admins.FirstOrDefaultAsync(a => a.Username == loginAdmin.Username);
            if (admin == null)
            {
                return Unauthorized("Invalid username or password.");
            }

            bool passwordMatch = BCrypt.Net.BCrypt.Verify(loginAdmin.Password, admin.PasswordHash);
            if (!passwordMatch)
            {
                return Unauthorized("Invalid username or password.");
            }

            var token = _tokenServices.GenerateJwtToken(admin);

            return Ok(new { token });
        }
    }
}
