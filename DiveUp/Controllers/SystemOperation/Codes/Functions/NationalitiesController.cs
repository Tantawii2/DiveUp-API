using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DiveUp.Data;
using DiveUp.DTOs.SystemOperation.Codes.Functions;
using DiveUp.Models.SystemOperation.Codes.Functions;

namespace DiveUp.Controllers.SystemOperation.Codes.Functions
{
    [ApiController][Route("api/[controller]")][Produces("application/json")]
    public class NationalitiesController : ControllerBase
    {
        private readonly AppDbContext _db;
        public NationalitiesController(AppDbContext db) => _db = db;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<NationalityDto>>> GetAll([FromQuery] string? search)
        {
            var q = _db.Nationalities.AsQueryable();
            if (!string.IsNullOrWhiteSpace(search)) { var s=search.Trim().ToLower(); q=q.Where(n=>n.NationalityName.ToLower().Contains(s)); }
            return Ok(await q.OrderBy(n=>n.NationalityName).Select(n=>ToDto(n)).ToListAsync());
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<NationalityDto>> GetById(int id)
        { var n=await _db.Nationalities.FindAsync(id); return n==null?NotFound(new{message=$"Nationality {id} not found."}):Ok(ToDto(n)); }
        [HttpPost]
        public async Task<ActionResult<NationalityDto>> Create([FromBody] NationalityCreateDto dto)
        { var n=new Nationality{NationalityName=dto.NationalityName,IsActive=dto.IsActive,RecordBy=dto.RecordBy,RecordTime=DateTime.UtcNow}; _db.Nationalities.Add(n); await _db.SaveChangesAsync(); return CreatedAtAction(nameof(GetById),new{id=n.Id},ToDto(n)); }
        [HttpPut("{id:int}")]
        public async Task<ActionResult<NationalityDto>> Update(int id, [FromBody] NationalityUpdateDto dto)
        { var n=await _db.Nationalities.FindAsync(id); if(n==null) return NotFound(new{message=$"Nationality {id} not found."}); n.NationalityName=dto.NationalityName; n.IsActive=dto.IsActive; n.RecordBy=dto.RecordBy; await _db.SaveChangesAsync(); return Ok(ToDto(n)); }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        { var n=await _db.Nationalities.FindAsync(id); if(n==null) return NotFound(new{message=$"Nationality {id} not found."}); _db.Nationalities.Remove(n); await _db.SaveChangesAsync(); return Ok(new{message=$"'{n.NationalityName}' deleted."}); }
        private static NationalityDto ToDto(Nationality n) => new(){Id=n.Id,NationalityName=n.NationalityName,IsActive=n.IsActive,RecordBy=n.RecordBy,RecordTime=n.RecordTime};
    }
}
