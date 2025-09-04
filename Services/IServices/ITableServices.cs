using ASP_Reservations.DTO;

namespace ASP_Reservations.Services.IServices
{
    public interface ITableServices
    {
        Task<List<TableSummaryDTO>> GetAllTableAsync();
        Task<TableSummaryDTO> GetTableByIdAsync(int id);
        Task<UpdateTableDTO> UpdateTableAsync(int id, UpdateTableDTO tableDto);
        Task<IEnumerable<TableDTO>> GetAvailableTablesAsync();
        Task<string> GetTableStatusAsync(int tableNum);
        Task<IEnumerable<TableDTO>> GetTablesByCapacityAsync(int capacity);
        Task<bool> SetTableAvailabilityAsync(int tableId, bool isAvailable);

    }
}
