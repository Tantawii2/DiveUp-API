using Microsoft.AspNetCore.Mvc; using Microsoft.EntityFrameworkCore;
using DiveUp.Data; using DiveUp.DTOs; using DiveUp.Models;
namespace DiveUp.Controllers
{
    [ApiController][Route("api/[controller]")][Produces("application/json")]
    public class GuidesController : ControllerBase
    {
        private readonly AppDbContext _db;
        public GuidesController(AppDbContext db) => _db = db;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GuideDto>>> GetAll([FromQuery] string? search)
        {
            var q = _db.Guides.AsQueryable();
            if (!string.IsNullOrWhiteSpace(search)) { var s=search.Trim().ToLower(); q=q.Where(g=>g.GuideName.ToLower().Contains(s)||(g.Phone!=null&&g.Phone.ToLower().Contains(s))); }
            return Ok(await q.OrderBy(g=>g.GuideName).Select(g=>ToDto(g)).ToListAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<GuideDto>> GetById(int id)
        { var g=await _db.Guides.FindAsync(id); return g==null?NotFound(new{message=$"Guide {id} not found."}):Ok(ToDto(g)); }

        [HttpPost]
        public async Task<ActionResult<GuideDto>> Create([FromBody] GuideCreateDto dto)
        {
            var g=new Guide{GuideName=dto.GuideName,Address=dto.Address,Phone=dto.Phone,IsActive=dto.IsActive,RecordBy=dto.RecordBy,RecordTime=DateTime.UtcNow};
            _db.Guides.Add(g); await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById),new{id=g.Id},ToDto(g));
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<GuideDto>> Update(int id, [FromBody] GuideUpdateDto dto)
        {
            var g=await _db.Guides.FindAsync(id);
            if(g==null) return NotFound(new{message=$"Guide {id} not found."});
            g.GuideName=dto.GuideName; g.Address=dto.Address; g.Phone=dto.Phone; g.IsActive=dto.IsActive; g.RecordBy=dto.RecordBy;
            await _db.SaveChangesAsync(); return Ok(ToDto(g));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        { var g=await _db.Guides.FindAsync(id); if(g==null) return NotFound(new{message=$"Guide {id} not found."}); _db.Guides.Remove(g); await _db.SaveChangesAsync(); return Ok(new{message=$"'{g.GuideName}' deleted."}); }

        private static GuideDto ToDto(Guide g) => new(){Id=g.Id,GuideName=g.GuideName,Address=g.Address,Phone=g.Phone,IsActive=g.IsActive,RecordBy=g.RecordBy,RecordTime=g.RecordTime};
    }
}
