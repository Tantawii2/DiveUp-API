using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DiveUp.Data;
using DiveUp.Models;
using DiveUp.DTOs;

namespace DiveUp.Controllers
{
    [ApiController][Route("api/[controller]")][Produces("application/json")]
    public class OperationRepsController : ControllerBase
    {
        private readonly AppDbContext _db;
        public OperationRepsController(AppDbContext db) => _db = db;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RepDto>>> GetAll([FromQuery] string? search)
        {
            var q = _db.Reps.Include(r=>r.Agent).AsQueryable();
            if (!string.IsNullOrWhiteSpace(search)) { var s=search.Trim().ToLower(); q=q.Where(r=>r.RepName.ToLower().Contains(s)||r.Agent!=null&&r.Agent.AgentName.ToLower().Contains(s)); }
            return Ok(await q.OrderBy(r=>r.RepName).Select(r=>ToDto(r)).ToListAsync());
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<RepDto>> GetById(int id)
        { var r=await _db.Reps.Include(x=>x.Agent).FirstOrDefaultAsync(x=>x.Id==id); return r==null?NotFound(new{message=$"Rep {id} not found."}):Ok(ToDto(r)); }
        [HttpPost]
        public async Task<ActionResult<RepDto>> Create([FromBody] RepCreateDto dto)
        {
            var r=new Rep{RepName=dto.RepName,AgentId=dto.AgentId,Address=dto.Address,Phone=dto.Phone,IsActive=dto.IsActive,RecordBy=dto.RecordBy,RecordTime=DateTime.UtcNow};
            _db.Reps.Add(r); await _db.SaveChangesAsync(); await _db.Entry(r).Reference(x=>x.Agent).LoadAsync();
            return CreatedAtAction(nameof(GetById),new{id=r.Id},ToDto(r));
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult<RepDto>> Update(int id, [FromBody] RepUpdateDto dto)
        {
            var r=await _db.Reps.Include(x=>x.Agent).FirstOrDefaultAsync(x=>x.Id==id);
            if(r==null) return NotFound(new{message=$"Rep {id} not found."});
            r.RepName=dto.RepName; r.AgentId=dto.AgentId; r.Address=dto.Address; r.Phone=dto.Phone; r.IsActive=dto.IsActive; r.RecordBy=dto.RecordBy;
            await _db.SaveChangesAsync(); await _db.Entry(r).Reference(x=>x.Agent).LoadAsync();
            return Ok(ToDto(r));
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        { var r=await _db.Reps.FindAsync(id); if(r==null) return NotFound(new{message=$"Rep {id} not found."}); _db.Reps.Remove(r); await _db.SaveChangesAsync(); return Ok(new{message=$"'{r.RepName}' deleted."}); }
        private static RepDto ToDto(Rep r) => new(){Id=r.Id,RepName=r.RepName,AgentId=r.AgentId,AgentName=r.Agent?.AgentName,Address=r.Address,Phone=r.Phone,IsActive=r.IsActive,RecordBy=r.RecordBy,RecordTime=r.RecordTime};
    }
}
