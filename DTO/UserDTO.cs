using System.ComponentModel.DataAnnotations;

namespace ASP_Reservations.DTO
{
    public class UserDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string? Phone { get; set; }
        public int TotalBookings { get; set; }

    }

    public class UserResponse
    {
        [Required]
        public int UserId { get; set; }
        public string Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
    }

    public class CreateUserDTO
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        public string Name { get; set; } = string.Empty;

        [EmailAddress(ErrorMessage = "Invalid email format")]
        [StringLength(100, ErrorMessage = "Email cannot exceed 100 characters")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [Phone(ErrorMessage = "Invalid phone number format")]
        [RegularExpression(@"\+46|0[7][0-9]{8}", ErrorMessage = "Phone number must start with +46 or 07 and be followed by 8 digits")]
        public string? Phone { get; set; }

    }

    public class UpdateUserDTO
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        [StringLength(100, ErrorMessage = "Email cannot exceed 100 characters")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [Phone(ErrorMessage = "Invalid phone number format")]
        [RegularExpression(@"\+46|0[7][0-9]{8}", ErrorMessage = "Phone number must start with +46 or 07 and be followed by 8 digits")]
        public string? Phone { get; set; }
    }
}
