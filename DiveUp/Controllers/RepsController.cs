using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DiveUp.Data;
using DiveUp.DTOs;
using DiveUp.Models;

namespace DiveUp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class RepsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RepsController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>Get all reps - optional search by name or agent name</summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RepDto>>> GetAll([FromQuery] string? search)
        {
            var query = _context.Reps.Include(r => r.Agent).AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                var s = search.Trim().ToLower();
                query = query.Where(r =>
                    r.RepName.ToLower().Contains(s) ||
                    (r.Agent != null && r.Agent.AgentName.ToLower().Contains(s))
                );
            }

            var list = await query
                .OrderBy(r => r.RepName)
                .Select(r => new RepDto
                {
                    Id = r.Id,
                    RepName = r.RepName,
                    AgentId = r.AgentId,
                    AgentName = r.Agent != null ? r.Agent.AgentName : null,
                    RecordBy = r.RecordBy,
                    RecordTime = r.RecordTime
                })
                .ToListAsync();

            return Ok(list);
        }

        /// <summary>Get rep by ID</summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<RepDto>> GetById(int id)
        {
            var r = await _context.Reps.Include(x => x.Agent).FirstOrDefaultAsync(x => x.Id == id);
            if (r == null)
                return NotFound(new { message = $"Rep with ID {id} not found." });

            return Ok(ToDto(r));
        }

        /// <summary>Create a new rep</summary>
        [HttpPost]
        public async Task<ActionResult<RepDto>> Create([FromBody] RepCreateDto dto)
        {
            var rep = new Rep
            {
                RepName = dto.RepName,
                AgentId = dto.AgentId,
                RecordBy = dto.RecordBy,
                RecordTime = DateTime.Now
            };

            _context.Reps.Add(rep);
            await _context.SaveChangesAsync();
            await _context.Entry(rep).Reference(x => x.Agent).LoadAsync();

            return CreatedAtAction(nameof(GetById), new { id = rep.Id }, ToDto(rep));
        }

        /// <summary>Update a rep</summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<RepDto>> Update(int id, [FromBody] RepUpdateDto dto)
        {
            var rep = await _context.Reps.Include(x => x.Agent).FirstOrDefaultAsync(x => x.Id == id);
            if (rep == null)
                return NotFound(new { message = $"Rep with ID {id} not found." });

            rep.RepName = dto.RepName;
            rep.AgentId = dto.AgentId;
            rep.RecordBy = dto.RecordBy;

            await _context.SaveChangesAsync();
            await _context.Entry(rep).Reference(x => x.Agent).LoadAsync();

            return Ok(ToDto(rep));
        }

        /// <summary>Delete a rep</summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var rep = await _context.Reps.FindAsync(id);
            if (rep == null)
                return NotFound(new { message = $"Rep with ID {id} not found." });

            _context.Reps.Remove(rep);
            await _context.SaveChangesAsync();

            return Ok(new { message = $"Rep '{rep.RepName}' deleted successfully." });
        }

        private static RepDto ToDto(Rep r) => new()
        {
            Id = r.Id,
            RepName = r.RepName,
            AgentId = r.AgentId,
            AgentName = r.Agent?.AgentName,
            RecordBy = r.RecordBy,
            RecordTime = r.RecordTime
        };
    }
}
