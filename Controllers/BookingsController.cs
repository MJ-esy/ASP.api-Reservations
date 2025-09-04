using ASP_Reservations.DTO;
using ASP_Reservations.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASP_Reservations.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingServices _bookingServices;
        public BookingsController(IBookingServices bookingServices)
        {
            _bookingServices = bookingServices;
        }

        [Authorize]
        [HttpGet("allBookings")]
        public async Task<ActionResult> GetAllBookings()
        {
            var bookings = await _bookingServices.GetAllBookingsAsync();
            return Ok(bookings);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult> GetBookingById(int id)
        {
            var booking = await _bookingServices.GetBookingbyId(id);
            if (booking == null)
            {
                return NotFound(new { message = "Booking not found." });
            }
            return Ok(booking);
        }

        [Authorize]
        [HttpGet("userBookings/{userId}")]
        public async Task<ActionResult> GetUserByUserId(int id)
        {
            var bookings = await _bookingServices.GetBookingsByUserIdAsync(id);
            return Ok(bookings);
        }

        [Authorize]
        [HttpGet("bookingsByDate")]
        public async Task<ActionResult> GetBookingsByDate(DateTime date)
        {
            var bookings = await _bookingServices.GetBookingByDateAsync(date);
            return Ok(bookings);
        }

        [Authorize]
        [HttpGet("bookingsToday")]
        public async Task<ActionResult> GetBookingsToday()
        {
            var bookings = await _bookingServices.GetTodayBookingsAsync();
            return Ok(bookings);
        }

        [Authorize]
        [HttpPost("createBooking")]
        public async Task<ActionResult> CreateBooking(CreateBookingDTO createbookingDto)
        {
            var createdBooking = await _bookingServices.CreateBookingAsync(createbookingDto);
            return Ok(createdBooking);
        }

        [Authorize]
        [HttpPut("updateBooking/{id}")]
        public async Task<ActionResult> UpdateBooking(int id, UpdateBookingDTO updatebookingDto)
        {
            var updatedBooking = await _bookingServices.UpdateBookingAsync(id, updatebookingDto);
            return Ok(updatedBooking);
        }

        [Authorize]
        [HttpDelete("deleteBooking/{id}")]
        public async Task<ActionResult> DeleteBooking(int id)
        {
            var result = await _bookingServices.DeleteBookingAsync(id);
            if (result)
            {
                return Ok(new { message = "Booking deleted successfully." });
            }
            return NotFound(new { message = "Booking not found." });
        }
    }
}
