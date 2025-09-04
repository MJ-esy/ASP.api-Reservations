using ASP_Reservations.Data;
using ASP_Reservations.Models;
using ASP_Reservations.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace ASP_Reservations.Repositories
{
    public class BookingRepo : IBookingRepo
    {
        private readonly AppDbContext _context;
        public BookingRepo(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Booking> CreateBookingAsync(Booking booking)
        {
            booking.CreatedAt = DateTime.UtcNow;
            _context.Booking.Add(booking);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return booking;
            }
            return null;
        }

        public async Task<bool> DeleteBookingAsync(int id)
        {
            var booking = await _context.Booking.FindAsync(id);
            if (booking == null)
            {
                return false; // Booking not found
            }
            _context.Booking.Remove(booking);
            await _context.SaveChangesAsync();
            return true; // Booking deleted successfully
        }

        public async Task<int?> FindFirstAndAvailableTableAsync(DateTime requestedTime, int guestNum)
        {
            var blockStart = requestedTime.AddHours(-2); //2hours before
            var blockEnd = requestedTime.AddHours(2); //2hours after
            var availableTable = await _context.Tables.Where(t => t.IsAvailable && t.Capacity >= guestNum)
                .OrderBy(t => t.Capacity)
                .FirstOrDefaultAsync(t => !_context.Booking.Any(b => b.TableIdFk == t.TableId
                        && b.Status != Models.Enums.BookingStatus.Cancelled
                        && b.StartDateTime < blockEnd
                        && b.StartDateTime.AddHours(2) > blockStart));
            return availableTable?.TableId;
        }

        public async Task<List<int>> GetAllAvailableTablesAsync(DateTime requestedTime, int guestNum)
        {
            var blockStart = requestedTime.AddHours(-2); //2hours before
            var blockEnd = requestedTime.AddHours(2); //2hours after
            var availableTables = await _context.Tables.Where(t => t.IsAvailable && t.Capacity >= guestNum)
                .OrderBy(t => t.Capacity)
                .Where(t => !_context.Booking.Any(b => b.TableIdFk == t.TableId
                        && b.Status != Models.Enums.BookingStatus.Cancelled
                        && b.StartDateTime < blockEnd
                        && b.StartDateTime.AddHours(2) > blockStart))
                .Select(t => t.TableId)
                .ToListAsync();
            return availableTables;

        }

        public async Task<List<Booking>> GetAllBookingsAsync()
        {
            return await _context.Booking.Include(b => b.User)
                 .Include(b => b.Table)
                 .OrderBy(b => b.StartDateTime)
                 .ToListAsync();

        }

        public async Task<Booking?> GetBookingByIdAsync(int id)
        {
            return await _context.Booking.Include(b => b.User)
                .Include(b => b.Table)
                .FirstOrDefaultAsync(b => b.BookingId == id);
        }

        public async Task<List<Booking>> GetBookingsByDateAsync(DateTime date)
        {
            var startOfDay = date.Date;
            var endOfDay = startOfDay.AddDays(1);

            return await _context.Booking.Include(b => b.User)
                .Include(b => b.Table)
                .Where(b => b.StartDateTime >= startOfDay && b.StartDateTime < endOfDay)
                .OrderBy(b => b.StartDateTime)
                .ToListAsync();
        }

        public async Task<List<Booking>> GetBookingsByUserIdAsync(int userId)
        {
            var bookings = await _context.Booking
                .Include(b => b.User)
                .Include(b => b.Table)
                .Where(b => b.UserIdFk == userId)
                .OrderBy(b => b.StartDateTime)
                .ToListAsync();
            return bookings;
        }

        public async Task<List<Booking>> GetActiveBookings()
        {
            var now = DateTime.UtcNow;
            return await _context.Booking.Include(b => b.User)
                .Include(b => b.Table)
                .Where(b => b.Status == Models.Enums.BookingStatus.Confirmed
                    && b.StartDateTime.AddHours(2) > now)
                .OrderBy(b => b.StartDateTime)
                .ToListAsync();
        }


        public async Task<IEnumerable<Booking>> GetBookingsInTimeRangeAsync(DateTime startTime, DateTime endTime)
        {
            return await _context.Booking.Include(b => b.User)
                .Include(b => b.Table)
                .Where(b => b.StartDateTime < endTime && b.StartDateTime.AddHours(2) > startTime)
                .OrderBy(b => b.StartDateTime)
                .ToListAsync();
        }

        public async Task<bool> HasBookingConflictAsync(int tableId, DateTime requestedTime, int? excludeBookingId = null)
        {
            var blockStart = requestedTime.AddHours(-2); //2hours before
            var blockEnd = requestedTime.AddHours(2); //2hours after
            var query = _context.Booking.Where(b => b.TableIdFk == tableId
                && b.Status != Models.Enums.BookingStatus.Cancelled
                && b.StartDateTime < blockEnd
                && b.StartDateTime.AddHours(2) > blockStart);
            if (excludeBookingId.HasValue)
            {
                query = query.Where(b => b.BookingId != excludeBookingId.Value);
            }
            return await query.AnyAsync();
        }

        public async Task<bool> IsTableAvailableAsync(int tableId, DateTime requestedTime)
        {
            var table = await _context.Tables.FindAsync(tableId);
            if (table == null || !table.IsAvailable) return false;

            var blockStart = requestedTime.AddHours(-2); //2hours before
            var blockEnd = requestedTime.AddHours(2); //2hours after
            var conflictExists = await _context.Booking.AnyAsync(b => b.TableIdFk == tableId
                && b.Status != Models.Enums.BookingStatus.Cancelled
                && b.StartDateTime < blockEnd
                && b.StartDateTime.AddHours(2) > blockStart);
            return !conflictExists;
        }

        public async Task<Booking?> UpdateBookingAsync(Booking booking)
        {
            booking.UpdatedAt = DateTime.UtcNow;
            _context.Booking.Update(booking);
            await _context.SaveChangesAsync();
            return booking;
        }
    }
}
