using ASP_Reservations.DTO;
using ASP_Reservations.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASP_Reservations.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  [Authorize]
  public class BookingsController : ControllerBase
  {
    private readonly IBookingServices _bookingServices;
    public BookingsController(IBookingServices bookingServices)
    {
      _bookingServices = bookingServices;
    }

    [HttpGet("allBookings")]
    public async Task<ActionResult> GetAllBookings()
    {
      var bookings = await _bookingServices.GetAllBookingsAsync();
      return Ok(bookings);
    }

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

    [HttpGet("userBookings/{userId}")]
    public async Task<ActionResult> GetUserByUserId(int id)
    {
      var bookings = await _bookingServices.GetBookingsByUserIdAsync(id);
      return Ok(bookings);
    }

    [HttpGet("bookingsByDate")]
    public async Task<ActionResult> GetBookingsByDate(DateTime date)
    {
      //if (!DateTime.TryParseExact(date, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out var parsedDate))
      //{
      //  return BadRequest("Invalid date format. Please use YYYY-MM-DD.");
      //}
      var bookings = await _bookingServices.GetBookingByDateAsync(date);
      return Ok(bookings);
    }

    [HttpGet("bookingsToday")]
    public async Task<ActionResult> GetBookingsToday()
    {
      var bookings = await _bookingServices.GetTodayBookingsAsync();
      return Ok(bookings);
    }

    [AllowAnonymous]
    [HttpPost("createBooking")]
    public async Task<ActionResult> CreateBooking(CreateBookingDTO createbookingDto)
    {
      var createdBooking = await _bookingServices.CreateBookingAsync(createbookingDto);
      return Ok(createdBooking);
    }

    [AllowAnonymous]
    [HttpPut("updateBooking/{id}")]
    public async Task<ActionResult> UpdateBooking(int id, UpdateBookingDTO updatebookingDto)
    {
      var updatedBooking = await _bookingServices.UpdateBookingAsync(id, updatebookingDto);
      return Ok(updatedBooking);
    }

    [AllowAnonymous]
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
