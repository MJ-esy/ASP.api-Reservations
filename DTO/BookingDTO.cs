using ASP_Reservations.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace ASP_Reservations.DTO
{
  public class BookingDTO
  {
    public int BookingId { get; set; }
    [Required]
    public DateTime StartDateTime { get; set; }
    public DateTime EndDateTime { get; set; }
    [Required]
    public int TableIdFk { get; set; }
    public TableDTO Table { get; set; }
    [Required]
    public int GuestNum { get; set; }
    [Required]
    public int UserIdFk { get; set; }
    public UserDTO User { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public BookingStatus Status { get; set; }

  }

  public class CreateBookingDTO
  {
    [Required]
    public int UserIdFk { get; set; }
    public string User { get; set; }
    [Required]
    public int TableIdFk { get; set; }
    [Required]
    public DateTime StartDateTime { get; set; }
    [Required]
    public int GuestNum { get; set; }
    [Required]
    public string Phone { get; set; }
  }

  public class UpdateBookingDTO
  {
    [Required]
    public int BookingId { get; set; }
    [Required(ErrorMessage = "Booking Date is required")]
    public DateTime StartDateTime { get; set; }
    [Required(ErrorMessage = "Number of guests is required.")]
    [Range(1, 8, ErrorMessage = "Please contact the restraurant for party size larger than 8.")]
    public int GuestNum { get; set; }
    public BookingStatus Status { get; set; }
  }

  public class AvailabilityRequestDTO
  {
    [Required(ErrorMessage = "Booking Date is required")]
    public DateTime StartDateTime { get; set; }
    [Required(ErrorMessage = "Number of guests is required.")]
    [Range(1, 8, ErrorMessage = "Please contact the restaurant for party size larger than 8.")]
    public int GuestNum { get; set; }
  }

  public class AvalabilityResponseDTO
  {
    public bool isAvailable { get; set; }
    public int? AvailableTableId { get; set; }
    public int? TableNum { get; set; }
    public List<int> AllAvailableTableIds { get; set; } = new();
    public string Message { get; set; } = string.Empty;
  }

  public class BookingSummaryDTO
  {
    public int BookingId { get; set; }
    public string Name { get; set; }
    public int TableNum { get; set; }
    public DateTime StartDateTime { get; set; }
    public int GuestNum { get; set; }
    public BookingStatus Status { get; set; }
  }
}
