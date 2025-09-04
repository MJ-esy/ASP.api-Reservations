using ASP_Reservations.Models;

namespace ASP_Reservations.Repositories.IRepositories
{
    public interface ITableRepo
    {
        Task<List<Table>> GetAllTableAsync();
        Task<Table> GetTableByIdAsync(int id);
        Task<Table> UpdateTableAsync(Table table);

        Task<IEnumerable<Table>> GetAvailableTablesAsync();
        Task<Table?> GetTableByNumberAsync(int tableNum);

        Task<bool> SetTableAvailabilityAsync(int tableId, bool isAvailable);
        Task<IEnumerable<Table>> GetTablesByCapacityAsync(int capacity);
    }
}
