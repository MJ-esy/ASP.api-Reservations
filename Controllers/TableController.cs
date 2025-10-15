using ASP_Reservations.DTO;
using ASP_Reservations.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASP_Reservations.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class TableController : ControllerBase
  {
    private readonly ITableServices _tableServices;
    public TableController(ITableServices tableServices)
    {
      _tableServices = tableServices;
    }

    [HttpGet("allTables")]
    public async Task<ActionResult> GetAllTables()
    {
      var tables = await _tableServices.GetAllTableAsync();
      return Ok(tables);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetTableById(int id)
    {
      try
      {
        var table = await _tableServices.GetTableByIdAsync(id);
        return Ok(table);
      }
      catch (Exception ex)
      {
        return NotFound(new { message = ex.Message });
      }
    }

    [HttpGet("getAvailableTables")]
    public async Task<ActionResult> GetAvailableTables()
    {
      var availableTables = await _tableServices.GetAvailableTablesAsync();
      if (!availableTables.Any())
      {
        return NotFound(new { message = "No available tables found." });
      }
      return Ok(availableTables);
    }

    [HttpGet("getTableStatus/{id}")]
    [Authorize]
    public async Task<ActionResult> GetTableStatus(int id)
    {
      try
      {
        var table = await _tableServices.GetTableByIdAsync(id);
        return Ok(new { TableId = table.TableId, Status = table.Status });
      }
      catch (Exception ex)
      {
        return NotFound(new { message = ex.Message });
      }
    }

    [HttpGet("getTableCapacity")]
    public async Task<ActionResult> GetTablesByCapacity([FromQuery] int capacity)
    {
      var tables = await _tableServices.GetTablesByCapacityAsync(capacity);
      return Ok(tables);

    }

    [HttpPatch("setTableAvailability/{id}")]
    public async Task<ActionResult<bool>> SetTableAvailability(int id, [FromBody] SetTableAvailabilityDTO tableAvailabilityDTO)
    {
      var result = await _tableServices.SetTableAvailabilityAsync(id, tableAvailabilityDTO.IsAvailable);

      return Ok(result);
    }

    [Authorize]
    [HttpPut("updateTable/{id}")]
    public async Task<ActionResult> UpdateTable(int id, [FromBody] UpdateTableDTO tableDTO)
    {
      // Ensure the DTO's TableId matches the route id
      id = tableDTO.TableId;
      var updatedTable = await _tableServices.UpdateTableAsync(id, tableDTO);
      return Ok(updatedTable);
    }

  }
}
