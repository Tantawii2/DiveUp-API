using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DiveUp.Data;
using DiveUp.Models;
using DiveUp.DTOs;

namespace DiveUp.Controllers
{
    [ApiController][Route("api/[controller]")][Produces("application/json")]
    public class OperationHotelDestinationsController : ControllerBase
    {
        private readonly AppDbContext _db;
        public OperationHotelDestinationsController(AppDbContext db) => _db = db;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<HotelDestinationDto>>> GetAll([FromQuery] string? search)
        {
            var q = _db.HotelDestinations.AsQueryable();
            if (!string.IsNullOrWhiteSpace(search)) { var s=search.Trim().ToLower(); q=q.Where(d=>d.DestinationName.ToLower().Contains(s)); }
            return Ok(await q.OrderBy(d=>d.DestinationName).Select(d=>ToDto(d)).ToListAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<HotelDestinationDto>> GetById(int id)
        { var d=await _db.HotelDestinations.FindAsync(id); return d==null?NotFound(new{message=$"HotelDestination {id} not found."}):Ok(ToDto(d)); }

        [HttpPost]
        public async Task<ActionResult<HotelDestinationDto>> Create([FromBody] HotelDestinationCreateDto dto)
        {
            var d=new HotelDestination{DestinationName=dto.DestinationName,IsActive=dto.IsActive,RecordBy=dto.RecordBy,RecordTime=DateTime.UtcNow};
            _db.HotelDestinations.Add(d); await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById),new{id=d.Id},ToDto(d));
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<HotelDestinationDto>> Update(int id, [FromBody] HotelDestinationUpdateDto dto)
        {
            var d=await _db.HotelDestinations.FindAsync(id);
            if(d==null) return NotFound(new{message=$"HotelDestination {id} not found."});
            d.DestinationName=dto.DestinationName; d.IsActive=dto.IsActive; d.RecordBy=dto.RecordBy;
            await _db.SaveChangesAsync(); return Ok(ToDto(d));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        { var d=await _db.HotelDestinations.FindAsync(id); if(d==null) return NotFound(new{message=$"HotelDestination {id} not found."}); _db.HotelDestinations.Remove(d); await _db.SaveChangesAsync(); return Ok(new{message=$"'{d.DestinationName}' deleted."}); }

        private static HotelDestinationDto ToDto(HotelDestination d) => new(){Id=d.Id,DestinationName=d.DestinationName,IsActive=d.IsActive,RecordBy=d.RecordBy,RecordTime=d.RecordTime};
    }
}
