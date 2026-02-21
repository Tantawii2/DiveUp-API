using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DiveUp.Data;
using DiveUp.Models;
using DiveUp.DTOs;

namespace DiveUp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class OperationBoatsController : ControllerBase
    {
        private readonly AppDbContext _db;
        public OperationBoatsController(AppDbContext db) => _db = db;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BoatDto>>> GetAll([FromQuery] string? search)
        {
            var q = _db.Boats.AsQueryable();
            if (!string.IsNullOrWhiteSpace(search)) { var s = search.Trim().ToLower(); q = q.Where(b => b.BoatName.ToLower().Contains(s)); }
            return Ok(await q.OrderBy(b => b.BoatName).Select(b => ToDto(b)).ToListAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<BoatDto>> GetById(int id)
        {
            var b = await _db.Boats.FindAsync(id);
            return b == null ? NotFound(new { message = $"Boat {id} not found." }) : Ok(ToDto(b));
        }

        [HttpPost]
        public async Task<ActionResult<BoatDto>> Create([FromBody] BoatCreateDto dto)
        {
            var boat = new Boat { BoatName=dto.BoatName, IsActive=dto.IsActive, RecordBy=dto.RecordBy, RecordTime=DateTime.UtcNow };
            _db.Boats.Add(boat); await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = boat.Id }, ToDto(boat));
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<BoatDto>> Update(int id, [FromBody] BoatUpdateDto dto)
        {
            var boat = await _db.Boats.FindAsync(id);
            if (boat == null) return NotFound(new { message = $"Boat {id} not found." });
            boat.BoatName=dto.BoatName; boat.IsActive=dto.IsActive; boat.RecordBy=dto.RecordBy;
            await _db.SaveChangesAsync(); return Ok(ToDto(boat));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var boat = await _db.Boats.FindAsync(id);
            if (boat == null) return NotFound(new { message = $"Boat {id} not found." });
            _db.Boats.Remove(boat); await _db.SaveChangesAsync();
            return Ok(new { message = $"Boat '{boat.BoatName}' deleted." });
        }

        private static BoatDto ToDto(Boat b) => new() { Id=b.Id, BoatName=b.BoatName, IsActive=b.IsActive, RecordBy=b.RecordBy, RecordTime=b.RecordTime };
    }
}
