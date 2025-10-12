using ASP_Reservations.DTO;
using ASP_Reservations.Models;
using ASP_Reservations.Repositories.IRepositories;
using ASP_Reservations.Services.IServices;

namespace ASP_Reservations.Services
{
  public class BookingServices : IBookingServices
  {
    private readonly IBookingRepo _bookingRepo;
    private readonly IUserRepo _userRepo;
    private readonly ITableRepo _tableRepo;
    public BookingServices(IBookingRepo bookingRepo, IUserRepo userRepo, ITableRepo tableRepo)
    {
      _bookingRepo = bookingRepo;
      _userRepo = userRepo;
      _tableRepo = tableRepo;
    }
    public async Task<List<BookingSummaryDTO>> GetAllBookingsAsync()
    {
      var bookings = await _bookingRepo.GetAllBookingsAsync();
      return bookings.Select(b => new BookingSummaryDTO
      {
        BookingId = b.BookingId,
        Name = b.User.Name,
        TableNum = b.Table.TableNum,
        StartDateTime = b.StartDateTime,
        GuestNum = b.GuestNum,
        Status = b.Status
      }).ToList();
    }
    public async Task<BookingDTO> GetBookingbyId(int id)
    {
      var booking = await _bookingRepo.GetBookingByIdAsync(id);
      if (booking == null)
      {
        throw new Exception("Booking not found.");
      }
      return new BookingDTO
      {
        BookingId = id,
        UserIdFk = booking.UserIdFk,
        User = new UserDTO
        {
          Name = booking.User.Name,
          Email = booking.User.Email,
          Phone = booking.User.Phone
        },
        Table = new TableDTO
        {
          TableId = booking.Table.TableId,
          TableNum = booking.Table.TableNum,
          Capacity = booking.Table.Capacity
        },
        StartDateTime = booking.StartDateTime,
        EndDateTime = booking.EndDateTime,
        GuestNum = booking.GuestNum,
        Status = booking.Status,
        CreatedAt = booking.CreatedAt,
        UpdatedAt = booking.UpdatedAt
      };
    }
    public async Task<CreateBookingDTO> CreateBookingAsync(CreateBookingDTO createbookingDto)
    {
      var user = await _userRepo.GetUserByIdAsync(createbookingDto.UserIdFk);
      if (user == null)
      {
        throw new Exception("User not found.");
      }
      var availableTableId = await _bookingRepo.FindFirstAndAvailableTableAsync(createbookingDto.StartDateTime, createbookingDto.GuestNum);

      if (!availableTableId.HasValue)
      {
        throw new Exception("No available table found for the requested time and guest number.");
      }

      var newBooking = new Booking
      {
        UserIdFk = createbookingDto.UserIdFk,
        TableIdFk = availableTableId.Value,
        StartDateTime = createbookingDto.StartDateTime,
        GuestNum = createbookingDto.GuestNum,
        Status = Models.Enums.BookingStatus.Confirmed,
        CreatedAt = DateTime.UtcNow
      };
      var createBooking = await _bookingRepo.CreateBookingAsync(newBooking);
      var createdBooking = await _bookingRepo.GetBookingByIdAsync(createBooking.BookingId);
      return new CreateBookingDTO
      {
        UserIdFk = createdBooking.UserIdFk,
        User = createdBooking.User.Name,
        TableIdFk = createdBooking.TableIdFk,
        StartDateTime = createdBooking.StartDateTime,
        GuestNum = createdBooking.GuestNum,
        Phone = createdBooking.User.Phone
      };
    }

    public async Task<UpdateBookingDTO> UpdateBookingAsync(int id, UpdateBookingDTO updatebookingDto)
    {
      var existingBooking = await _bookingRepo.GetBookingByIdAsync(id);
      if (existingBooking == null)
      {
        throw new Exception("Booking not found.");
      }
      if (existingBooking.GuestNum != updatebookingDto.GuestNum || existingBooking.StartDateTime != updatebookingDto.StartDateTime)
      {
        var availableTableId = await _bookingRepo.FindFirstAndAvailableTableAsync(updatebookingDto.StartDateTime, updatebookingDto.GuestNum);
        if (!availableTableId.HasValue)
        {
          throw new Exception("No available table found for the requested time and guest number.");
        }
        existingBooking.TableIdFk = availableTableId.Value;
      }
      existingBooking.StartDateTime = updatebookingDto.StartDateTime;
      existingBooking.GuestNum = updatebookingDto.GuestNum;
      existingBooking.Status = updatebookingDto.Status;
      existingBooking.UpdatedAt = DateTime.UtcNow;
      var updatedBooking = await _bookingRepo.UpdateBookingAsync(existingBooking);
      return new UpdateBookingDTO
      {
        BookingId = updatedBooking.BookingId,
        StartDateTime = updatedBooking.StartDateTime,
        GuestNum = updatedBooking.GuestNum,
        Status = updatedBooking.Status
      };

    }
    public async Task<bool> DeleteBookingAsync(int id)
    {
      var existingBooking = await _bookingRepo.GetBookingByIdAsync(id);
      if (existingBooking == null)
      {
        throw new Exception("Booking not found.");
      }
      var result = await _bookingRepo.DeleteBookingAsync(id);
      return result;
    }
    public async Task<bool> CancelBookingAsync(int id)
    {
      var booking = await _bookingRepo.GetBookingByIdAsync(id);
      if (booking == null)
      {
        throw new Exception("Booking not found.");
        return false;
      }
      booking.Status = Models.Enums.BookingStatus.Cancelled;
      var updatedBooking = await _bookingRepo.UpdateBookingAsync(booking);
      return true;
    }

    public async Task<IEnumerable<BookingDTO>> GetBookingsByUserIdAsync(int userId)
    {
      var user = await _userRepo.GetUserByIdAsync(userId);
      if (user == null)
      {
        throw new Exception("User not found.");
      }
      var bookings = await _bookingRepo.GetBookingsByUserIdAsync(userId);
      return bookings.Select(b => new BookingDTO
      {
        BookingId = b.BookingId,
        TableIdFk = b.TableIdFk,
        StartDateTime = b.StartDateTime,
        EndDateTime = b.EndDateTime,
        GuestNum = b.GuestNum,
        Status = b.Status
      }).ToList();
    }
    public async Task<IEnumerable<BookingDTO>> GetBookingByDateAsync(DateTime date)
    {
      var bookings = await _bookingRepo.GetBookingsByDateAsync(date);
      return bookings.Select(b => new BookingDTO
      {
        BookingId = b.BookingId,
        TableIdFk = b.TableIdFk,
        UserIdFk = b.UserIdFk,
        User = new UserDTO
        {
          Name = b.User.Name,
          Email = b.User.Email,
          Phone = b.User.Phone
        },
        Table = new TableDTO
        {
          TableId = b.Table.TableId,
          TableNum = b.Table.TableNum,
          Capacity = b.Table.Capacity
        },
        StartDateTime = b.StartDateTime,
        EndDateTime = b.EndDateTime,
        GuestNum = b.GuestNum,
        Status = b.Status
      }).ToList();

    }

    public async Task<IEnumerable<BookingSummaryDTO>> GetTodayBookingsAsync()
    {
      var bookings = await _bookingRepo.GetBookingsByDateAsync(DateTime.UtcNow);
      return bookings.Select(b => new BookingSummaryDTO
      {
        BookingId = b.BookingId,
        Name = b.User.Name,
        TableNum = b.Table.TableNum,
        StartDateTime = b.StartDateTime,
        GuestNum = b.GuestNum,
        Status = b.Status
      }).ToList();
    }
    public async Task<IEnumerable<BookingSummaryDTO>> GetActiveBookingsAsync()
    {
      var activeBooking = await _bookingRepo.GetActiveBookings();
      return activeBooking.Select(b => new BookingSummaryDTO
      {
        BookingId = b.BookingId,
        Name = b.User.Name,
        TableNum = b.Table.TableNum,
        StartDateTime = b.StartDateTime,
        GuestNum = b.GuestNum,
        Status = b.Status
      }).ToList();
    }
  }
}
