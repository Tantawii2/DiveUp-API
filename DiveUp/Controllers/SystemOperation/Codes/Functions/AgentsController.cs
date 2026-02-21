using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DiveUp.Data;
using DiveUp.DTOs;
using DiveUp.Models;

namespace DiveUp.Controllers.SystemOperation.Codes.Functions
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class AgentsController : ControllerBase
    {
        private readonly AppDbContext _db;
        public AgentsController(AppDbContext db) => _db = db;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AgentDto>>> GetAll([FromQuery] string? search)
        {
            var q = _db.Agents.Include(a => a.Nationality).AsQueryable();
            if (!string.IsNullOrWhiteSpace(search))
            {
                var s = search.Trim().ToLower();
                q = q.Where(a => a.AgentName.ToLower().Contains(s) || a.AgentCode.ToLower().Contains(s));
            }
            return Ok(await q.OrderBy(a => a.AgentName).Select(a => ToDto(a)).ToListAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<AgentDto>> GetById(int id)
        {
            var a = await _db.Agents.Include(x => x.Nationality).FirstOrDefaultAsync(x => x.Id == id);
            return a == null ? NotFound(new { message = $"Agent {id} not found." }) : Ok(ToDto(a));
        }

        [HttpPost]
        public async Task<ActionResult<AgentDto>> Create([FromBody] AgentCreateDto dto)
        {
            if (await _db.Agents.AnyAsync(a => a.AgentCode == dto.AgentCode))
                return Conflict(new { message = $"AgentCode '{dto.AgentCode}' already exists." });
            var agent = new Agent { AgentCode=dto.AgentCode, AgentName=dto.AgentName, NationalityId=dto.NationalityId, VatNo=dto.VatNo, FileNo=dto.FileNo, Email=dto.Email, Address=dto.Address, Phone=dto.Phone, IsActive=dto.IsActive, RecordBy=dto.RecordBy, RecordTime=DateTime.UtcNow };
            _db.Agents.Add(agent);
            await _db.SaveChangesAsync();
            await _db.Entry(agent).Reference(x => x.Nationality).LoadAsync();
            return CreatedAtAction(nameof(GetById), new { id = agent.Id }, ToDto(agent));
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<AgentDto>> Update(int id, [FromBody] AgentUpdateDto dto)
        {
            var agent = await _db.Agents.Include(x=>x.Nationality).FirstOrDefaultAsync(x=>x.Id==id);
            if (agent == null) return NotFound(new { message = $"Agent {id} not found." });
            if (await _db.Agents.AnyAsync(a => a.AgentCode == dto.AgentCode && a.Id != id))
                return Conflict(new { message = $"AgentCode '{dto.AgentCode}' used by another agent." });
            agent.AgentCode=dto.AgentCode; agent.AgentName=dto.AgentName; agent.NationalityId=dto.NationalityId;
            agent.VatNo=dto.VatNo; agent.FileNo=dto.FileNo; agent.Email=dto.Email; agent.Address=dto.Address;
            agent.Phone=dto.Phone; agent.IsActive=dto.IsActive; agent.RecordBy=dto.RecordBy;
            await _db.SaveChangesAsync();
            await _db.Entry(agent).Reference(x => x.Nationality).LoadAsync();
            return Ok(ToDto(agent));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var agent = await _db.Agents.FindAsync(id);
            if (agent == null) return NotFound(new { message = $"Agent {id} not found." });
            _db.Agents.Remove(agent);
            await _db.SaveChangesAsync();
            return Ok(new { message = $"Agent '{agent.AgentName}' deleted." });
        }

        private static AgentDto ToDto(Agent a) => new()
        {
            Id=a.Id, AgentCode=a.AgentCode, AgentName=a.AgentName, NationalityId=a.NationalityId,
            NationalityName=a.Nationality?.NationalityName, VatNo=a.VatNo, FileNo=a.FileNo,
            Email=a.Email, Address=a.Address, Phone=a.Phone, IsActive=a.IsActive,
            RecordBy=a.RecordBy, RecordTime=a.RecordTime
        };
    }
}
