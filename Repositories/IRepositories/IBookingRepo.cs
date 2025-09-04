using ASP_Reservations.Models;

namespace ASP_Reservations.Repositories.IRepositories
{
    public interface IBookingRepo
    {
        Task<List<Booking>> GetAllBookingsAsync();
        Task<Booking?> GetBookingByIdAsync(int id);
        Task<Booking> CreateBookingAsync(Booking booking);
        Task<Booking?> UpdateBookingAsync(Booking booking);
        Task<bool> DeleteBookingAsync(int id);
        Task<List<Booking>> GetBookingsByUserIdAsync(int userId);
        Task<List<Booking>> GetBookingsByDateAsync(DateTime date);
        Task<List<Booking>> GetActiveBookings();
        Task<int?> FindFirstAndAvailableTableAsync(DateTime requestedTime, int guestNum);
        Task<bool> IsTableAvailableAsync(int tableId, DateTime requestedTime);
        Task<List<int>> GetAllAvailableTablesAsync(DateTime requestedTime, int guestNum);
        Task<IEnumerable<Booking>> GetBookingsInTimeRangeAsync(DateTime startTime, DateTime endTime);
        Task<bool> HasBookingConflictAsync(int tableId, DateTime requestedTime, int? excludeBookingId = null);
    }
}
