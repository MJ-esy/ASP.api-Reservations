using ASP_Reservations.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace ASP_Reservations.Models
{
    public class Booking
    {
        [Key]
        public int BookingId { get; set; }

        public int UserIdFk { get; set; }
        public virtual User? User { get; set; }

        public int TableIdFk { get; set; }
        public virtual Table? Table { get; set; }

        [Required]
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime => StartDateTime.AddHours(2);       // always StartTime + 2h
        [Required]
        public int GuestNum { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public BookingStatus Status { get; set; }


    }


}
