using ASP_Reservations.DTO;
using ASP_Reservations.Repositories.IRepositories;
using ASP_Reservations.Services.IServices;

namespace ASP_Reservations.Services
{
    public class TableServices : ITableServices
    {
        private readonly ITableRepo _tableRepo;
        private readonly IBookingRepo _bookingRepo;
        public TableServices(ITableRepo tableRepo, IBookingRepo bookingRepo)
        {
            _tableRepo = tableRepo;
            _bookingRepo = bookingRepo;
        }

        public async Task<List<TableSummaryDTO>> GetAllTableAsync()
        {
            var tables = await _tableRepo.GetAllTableAsync();
            var tableDTOs = tables.Select(t => new TableSummaryDTO
            {
                TableId = t.TableId,
                TableNum = t.TableNum,
                Capacity = t.Capacity,
                IsAvailable = t.IsAvailable,
                Status = t.IsAvailable ? "Available" : "Occupied"

            }).ToList();
            return tableDTOs;
        }

        public async Task<IEnumerable<TableDTO>> GetAvailableTablesAsync()
        {
            var availableTables = await _tableRepo.GetAvailableTablesAsync();
            var availableTableDTOs = availableTables.Select(t => new TableDTO
            {
                TableId = t.TableId,
                TableNum = t.TableNum,
                Capacity = t.Capacity,
            });
            return availableTableDTOs;
        }

        public async Task<TableSummaryDTO> GetTableByIdAsync(int id)
        {
            var table = await _tableRepo.GetTableByIdAsync(id);
            if (table == null)
            {
                throw new Exception("Table not found.");
            }
            var tableDto = new TableSummaryDTO
            {
                TableId = table.TableId,
                TableNum = table.TableNum,
                Capacity = table.Capacity,
                IsAvailable = table.IsAvailable,
                Status = table.IsAvailable ? "Available" : "Occupied"
            };
            return tableDto;
        }

        public async Task<string> GetTableStatusAsync(int tableNum)
        {
            var table = await _tableRepo.GetTableByIdAsync(tableNum);
            if (table == null)
            {
                throw new Exception("Table not found.");
            }
            if (!table.IsAvailable)
            {
                return "Occupied";
            }
            var now = DateTime.Now;
            var currentBoolings = await _bookingRepo.GetBookingsInTimeRangeAsync(now.AddHours(-2), now.AddHours(2));
            var isCurrentlyBooked = currentBoolings.Any(b => b.TableIdFk == table.TableId && b.Status.ToString() == "Confirmed"
                                           && b.StartDateTime <= now
                                           && b.StartDateTime.AddHours(2) > now);

            return isCurrentlyBooked ? "Occupied" : "Available";
        }

        public async Task<IEnumerable<TableDTO>> GetTablesByCapacityAsync(int capacity)
        {
            var tablesByCapacity = await _tableRepo.GetTablesByCapacityAsync(capacity);
            var tableDTOs = tablesByCapacity.Select(t => new TableDTO
            {
                TableId = t.TableId,
                TableNum = t.TableNum,
                Capacity = t.Capacity,
            }).ToList();
            return tableDTOs;
        }

        public async Task<bool> SetTableAvailabilityAsync(int tableId, bool isAvailable)
        {
            if (!isAvailable)
            {
                var activeBookings = await _bookingRepo.GetActiveBookings();
                var hasActiveBooking = activeBookings.Any(b => b.TableIdFk == tableId);

                if (hasActiveBooking)
                {
                    throw new Exception("Cannot set table to available. There are active bookings for this table.");
                }
            }
            return await _tableRepo.SetTableAvailabilityAsync(tableId, isAvailable);
        }

        public async Task<UpdateTableDTO> UpdateTableAsync(int id, UpdateTableDTO tableDto)
        {
            var table = await _tableRepo.GetTableByIdAsync(id);
            if (table == null)
            {
                throw new Exception("Table not found.");
            }

            table.TableId = id;
            table.TableNum = tableDto.TableNum;
            table.Capacity = tableDto.Capacity;
            table.IsAvailable = tableDto.IsAvailable;

            await _tableRepo.UpdateTableAsync(table);
            return tableDto;
        }
    }
}
