using System.ComponentModel.DataAnnotations;

namespace ASP_Reservations.DTO
{
    public class TableDTO
    {
        public int TableId { get; set; }
        public int TableNum { get; set; }
        public int Capacity { get; set; }
    }


    public class UpdateTableDTO
    {
        [Required]
        public int TableId { get; set; }
        [Required(ErrorMessage = "Table Number is required")]
        public int TableNum { get; set; }
        [Required(ErrorMessage = "Capacity is required")]
        public int Capacity { get; set; }
        [Required]
        public bool IsAvailable { get; set; }
    }

    public class TableSummaryDTO
    {
        public int TableId { get; set; }
        public int TableNum { get; set; }
        public int Capacity { get; set; }
        public bool IsAvailable { get; set; }
        public string Status { get; set; } = string.Empty;
    }

    public class SetTableAvailabilityDTO
    {
        [Required]
        public bool IsAvailable { get; set; }
    }
}
