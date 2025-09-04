using ASP_Reservations.Data;
using ASP_Reservations.Models;
using ASP_Reservations.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace ASP_Reservations.Repositories
{
    public class TableRepo : ITableRepo
    {
        private readonly AppDbContext _context;
        public TableRepo(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Table>> GetAllTableAsync()
        {
            var tables = await _context.Tables.OrderBy(t => t.TableNum)
                .ToListAsync();
            return tables; // Return the list of tables
        }

        public async Task<IEnumerable<Table>> GetAvailableTablesAsync()
        {
            var availableTables = await _context.Tables.Where(t => t.IsAvailable)
                .ToListAsync();
            return availableTables;
        }

        public async Task<Table> GetTableByIdAsync(int id)
        {
            var table = await _context.Tables.Include(t => t.Bookings)
                .FirstOrDefaultAsync(t => t.TableId == id);
            return table;
        }

        public async Task<Table?> GetTableByNumberAsync(int tableNum)
        {
            var tableWithNum = await _context.Tables.FirstOrDefaultAsync(t => t.TableNum == tableNum);
            return tableWithNum;
        }

        public async Task<IEnumerable<Table>> GetTablesByCapacityAsync(int capacity)
        {
            var tablesByCapacity = await _context.Tables.Where(t => t.Capacity >= capacity)
                .OrderBy(t => t.Capacity)
                .ToListAsync();
            return tablesByCapacity;
        }

        public async Task<bool> SetTableAvailabilityAsync(int tableId, bool isAvailable)
        {
            var table = await _context.Tables.FindAsync(tableId);
            if (table == null)
            {
                return false; // Table not found
            }
            table.IsAvailable = isAvailable;
            await _context.SaveChangesAsync();
            return true; // Availability updated successfully
        }

        public async Task<Table> UpdateTableAsync(Table table)
        {
            var tableToUpdate = _context.Tables.Update(table);
            var existingTable = await _context.SaveChangesAsync();

            return tableToUpdate.Entity; // Return the updated table object

        }
    }
}
