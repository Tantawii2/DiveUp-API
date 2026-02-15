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
    public class AgentsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AgentsController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>Get all agents - optional search by name, code, or nationality</summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AgentDto>>> GetAll([FromQuery] string? search)
        {
            var query = _context.Agents.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                var s = search.Trim().ToLower();
                query = query.Where(a =>
                    a.AgentName.ToLower().Contains(s) ||
                    a.AgentCode.ToLower().Contains(s) ||
                    (a.Nationality != null && a.Nationality.ToLower().Contains(s)) ||
                    (a.Email != null && a.Email.ToLower().Contains(s)) ||
                    (a.Phone != null && a.Phone.ToLower().Contains(s))
                );
            }

            var list = await query
                .OrderBy(a => a.AgentName)
                .Select(a => new AgentDto
                {
                    Id = a.Id,
                    AgentCode = a.AgentCode,
                    AgentName = a.AgentName,
                    Nationality = a.Nationality,
                    VatNo = a.VatNo,
                    FileNo = a.FileNo,
                    Email = a.Email,
                    Address = a.Address,
                    Phone = a.Phone,
                    RecordBy = a.RecordBy,
                    RecordTime = a.RecordTime
                })
                .ToListAsync();

            return Ok(list);
        }

        /// <summary>Get agent by ID</summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<AgentDto>> GetById(int id)
        {
            var a = await _context.Agents.FindAsync(id);
            if (a == null)
                return NotFound(new { message = $"Agent with ID {id} not found." });

            return Ok(ToDto(a));
        }

        /// <summary>Create a new agent</summary>
        [HttpPost]
        public async Task<ActionResult<AgentDto>> Create([FromBody] AgentCreateDto dto)
        {
            bool exists = await _context.Agents.AnyAsync(a => a.AgentCode == dto.AgentCode);
            if (exists)
                return Conflict(new { message = $"Agent Code '{dto.AgentCode}' already exists." });

            var agent = new Agent
            {
                AgentCode = dto.AgentCode,
                AgentName = dto.AgentName,
                Nationality = dto.Nationality,
                VatNo = dto.VatNo,
                FileNo = dto.FileNo,
                Email = dto.Email,
                Address = dto.Address,
                Phone = dto.Phone,
                RecordBy = dto.RecordBy,
                RecordTime = DateTime.Now
            };

            _context.Agents.Add(agent);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = agent.Id }, ToDto(agent));
        }

        /// <summary>Update an existing agent</summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<AgentDto>> Update(int id, [FromBody] AgentUpdateDto dto)
        {
            var agent = await _context.Agents.FindAsync(id);
            if (agent == null)
                return NotFound(new { message = $"Agent with ID {id} not found." });

            bool codeUsed = await _context.Agents.AnyAsync(a => a.AgentCode == dto.AgentCode && a.Id != id);
            if (codeUsed)
                return Conflict(new { message = $"Agent Code '{dto.AgentCode}' is already used by another agent." });

            agent.AgentCode = dto.AgentCode;
            agent.AgentName = dto.AgentName;
            agent.Nationality = dto.Nationality;
            agent.VatNo = dto.VatNo;
            agent.FileNo = dto.FileNo;
            agent.Email = dto.Email;
            agent.Address = dto.Address;
            agent.Phone = dto.Phone;
            agent.RecordBy = dto.RecordBy;

            await _context.SaveChangesAsync();
            return Ok(ToDto(agent));
        }

        /// <summary>Delete an agent</summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var agent = await _context.Agents.FindAsync(id);
            if (agent == null)
                return NotFound(new { message = $"Agent with ID {id} not found." });

            _context.Agents.Remove(agent);
            await _context.SaveChangesAsync();

            return Ok(new { message = $"Agent '{agent.AgentName}' deleted successfully." });
        }

        private static AgentDto ToDto(Agent a) => new()
        {
            Id = a.Id,
            AgentCode = a.AgentCode,
            AgentName = a.AgentName,
            Nationality = a.Nationality,
            VatNo = a.VatNo,
            FileNo = a.FileNo,
            Email = a.Email,
            Address = a.Address,
            Phone = a.Phone,
            RecordBy = a.RecordBy,
            RecordTime = a.RecordTime
        };
    }
}
