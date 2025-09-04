namespace ASP_Reservations.DTO
{
    public class AdminDTO
    {
        public string Username { get; set; } = null;
        public string Password { get; set; } = null;
        
    }

    public class LoginAdminDTO
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; }

    }

}
