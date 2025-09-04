using System.ComponentModel.DataAnnotations;

namespace ASP_Reservations.Models
{
    public class Table
    {
        [Key]
        public int TableId { get; set; }
        public int TableNum { get; set; }
        public int Capacity { get; set; }
        public bool IsAvailable { get; set; } = true;

        public List<Booking> Bookings { get; set; }
    }
}
