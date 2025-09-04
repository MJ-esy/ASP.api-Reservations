using ASP_Reservations.DTO;

namespace ASP_Reservations.Services.IServices
{
    public interface IBookingServices
    {
        Task<List<BookingSummaryDTO>> GetAllBookingsAsync();
        Task<BookingDTO> GetBookingbyId(int id);
        Task<CreateBookingDTO> CreateBookingAsync(CreateBookingDTO createbookingDto);
        Task<UpdateBookingDTO> UpdateBookingAsync(int id, UpdateBookingDTO updatebookingDto);
        Task<bool> DeleteBookingAsync(int id);
        Task<bool> CancelBookingAsync(int id);

        Task<IEnumerable<BookingDTO>> GetBookingsByUserIdAsync(int userId);
        Task<IEnumerable<BookingDTO>> GetBookingByDateAsync(DateTime date);

        Task<IEnumerable<BookingSummaryDTO>> GetTodayBookingsAsync();
        Task<IEnumerable<BookingSummaryDTO>> GetActiveBookingsAsync();
    }
}
