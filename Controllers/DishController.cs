using ASP_Reservations.DTO;
using ASP_Reservations.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASP_Reservations.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DishController : ControllerBase
    {
        private readonly IDishServices _dishServices;
        public DishController(IDishServices dishServices)
        {
            _dishServices = dishServices;
        }

        [HttpGet("allDishes")]
        public async Task<ActionResult> GetAllDishes()
        {
            var dishes = await _dishServices.GetAllDishesAsync();
            return Ok(dishes);
        }

        [Authorize]
        [HttpGet("id")]
        public async Task<ActionResult> GetDishById(int id)
        {
            var dish = await _dishServices.GetDishByIdAsync(id);
            if (dish == null)
            {
                return NotFound(new { message = "Dish not found." });
            }
            return Ok(dish);
        }

        [Authorize]
        [HttpPost("createNewDish")]
        public async Task<ActionResult> CreateDish(CreateDishDTO dishDTO)
        {
            var result = await _dishServices.CreateDishAsync(dishDTO);
            if (result)
            {
                return Ok(new { message = "Dish created successfully!" });
            }
            return BadRequest(new { message = "Failed to create dish." });
        }

        [Authorize]
        [HttpPut("updateDish/{id}")]
        public async Task<ActionResult> UpdateDish(int id, UpdateDishDTO dishDTO)
        {
            var result = await _dishServices.UpdateDishAsync(id, dishDTO);
            if (result)
            {
                return Ok(new { message = "Dish updated successfully!" });
            }
            return BadRequest(new { message = "Failed to update dish." });
        }

        [Authorize]
        [HttpDelete("deleteDish/{id}")]
        public async Task<ActionResult> DeleteDish(int id)
        {
            var result = await _dishServices.DeleteDishAsync(id);
            if (result)
            {
                return Ok(new { message = "Dish deleted successfully!" });
            }
            return BadRequest(new { message = "Failed to delete dish." });
        }
    }

}

