using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DiveUp.Data;
using DiveUp.Models;
using DiveUp.DTOs;

namespace DiveUp.Controllers
{
    [ApiController][Route("api/[controller]")][Produces("application/json")]
    public class OperationHotelsController : ControllerBase
    {
        private readonly AppDbContext _db;
        public OperationHotelsController(AppDbContext db) => _db = db;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<HotelDto>>> GetAll([FromQuery] string? search)
        {
            var q = _db.Hotels.Include(h=>h.Destination).AsQueryable();
            if (!string.IsNullOrWhiteSpace(search)) { var s=search.Trim().ToLower(); q=q.Where(h=>h.HotelName.ToLower().Contains(s)||h.Destination!=null&&h.Destination.DestinationName.ToLower().Contains(s)); }
            return Ok(await q.OrderBy(h=>h.HotelName).Select(h=>ToDto(h)).ToListAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<HotelDto>> GetById(int id)
        { var h=await _db.Hotels.Include(x=>x.Destination).FirstOrDefaultAsync(x=>x.Id==id); return h==null?NotFound(new{message=$"Hotel {id} not found."}):Ok(ToDto(h)); }

        [HttpPost]
        public async Task<ActionResult<HotelDto>> Create([FromBody] HotelCreateDto dto)
        {
            var h=new Hotel{HotelName=dto.HotelName,DestinationId=dto.DestinationId,IsActive=dto.IsActive,RecordBy=dto.RecordBy,RecordTime=DateTime.UtcNow};
            _db.Hotels.Add(h); await _db.SaveChangesAsync();
            await _db.Entry(h).Reference(x=>x.Destination).LoadAsync();
            return CreatedAtAction(nameof(GetById),new{id=h.Id},ToDto(h));
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<HotelDto>> Update(int id, [FromBody] HotelUpdateDto dto)
        {
            var h=await _db.Hotels.Include(x=>x.Destination).FirstOrDefaultAsync(x=>x.Id==id);
            if(h==null) return NotFound(new{message=$"Hotel {id} not found."});
            h.HotelName=dto.HotelName; h.DestinationId=dto.DestinationId; h.IsActive=dto.IsActive; h.RecordBy=dto.RecordBy;
            await _db.SaveChangesAsync(); await _db.Entry(h).Reference(x=>x.Destination).LoadAsync();
            return Ok(ToDto(h));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        { var h=await _db.Hotels.FindAsync(id); if(h==null) return NotFound(new{message=$"Hotel {id} not found."}); _db.Hotels.Remove(h); await _db.SaveChangesAsync(); return Ok(new{message=$"'{h.HotelName}' deleted."}); }

        private static HotelDto ToDto(Hotel h) => new(){Id=h.Id,HotelName=h.HotelName,DestinationId=h.DestinationId,DestinationName=h.Destination?.DestinationName,IsActive=h.IsActive,RecordBy=h.RecordBy,RecordTime=h.RecordTime};
    }
}
