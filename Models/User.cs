using System.ComponentModel.DataAnnotations;

namespace ASP_Reservations.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string? Phone { get; set; }

        public List<Booking> Bookings { get; set; }

    }
}
