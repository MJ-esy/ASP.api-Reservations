using System.ComponentModel.DataAnnotations;

namespace ASP_Reservations.Models
{
    public class Admin
    {
        [Key]
        public int AdminId { get; set; }

        [MaxLength(60)] 
        public string Username { get; set; }  
        public string PasswordHash { get; set; }

        public string Role { get; set; } = "ADMIN";
    }
}
