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
    public class GuidesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public GuidesController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>Get all guides - optional search by name, phone, or address</summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GuideDto>>> GetAll([FromQuery] string? search)
        {
            var query = _context.Guides.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                var s = search.Trim().ToLower();
                query = query.Where(g =>
                    g.GuideName.ToLower().Contains(s) ||
                    (g.Phone != null && g.Phone.ToLower().Contains(s)) ||
                    (g.Address != null && g.Address.ToLower().Contains(s))
                );
            }

            var list = await query
                .OrderBy(g => g.GuideName)
                .Select(g => new GuideDto
                {
                    Id = g.Id,
                    GuideName = g.GuideName,
                    Address = g.Address,
                    Phone = g.Phone,
                    RecordBy = g.RecordBy,
                    RecordTime = g.RecordTime
                })
                .ToListAsync();

            return Ok(list);
        }

        /// <summary>Get guide by ID</summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<GuideDto>> GetById(int id)
        {
            var g = await _context.Guides.FindAsync(id);
            if (g == null)
                return NotFound(new { message = $"Guide with ID {id} not found." });

            return Ok(ToDto(g));
        }

        /// <summary>Create a new guide</summary>
        [HttpPost]
        public async Task<ActionResult<GuideDto>> Create([FromBody] GuideCreateDto dto)
        {
            var guide = new Guide
            {
                GuideName = dto.GuideName,
                Address = dto.Address,
                Phone = dto.Phone,
                RecordBy = dto.RecordBy,
                RecordTime = DateTime.Now
            };

            _context.Guides.Add(guide);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = guide.Id }, ToDto(guide));
        }

        /// <summary>Update a guide</summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<GuideDto>> Update(int id, [FromBody] GuideUpdateDto dto)
        {
            var guide = await _context.Guides.FindAsync(id);
            if (guide == null)
                return NotFound(new { message = $"Guide with ID {id} not found." });

            guide.GuideName = dto.GuideName;
            guide.Address = dto.Address;
            guide.Phone = dto.Phone;
            guide.RecordBy = dto.RecordBy;

            await _context.SaveChangesAsync();
            return Ok(ToDto(guide));
        }

        /// <summary>Delete a guide</summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var guide = await _context.Guides.FindAsync(id);
            if (guide == null)
                return NotFound(new { message = $"Guide with ID {id} not found." });

            _context.Guides.Remove(guide);
            await _context.SaveChangesAsync();

            return Ok(new { message = $"Guide '{guide.GuideName}' deleted successfully." });
        }

        private static GuideDto ToDto(Guide g) => new()
        {
            Id = g.Id,
            GuideName = g.GuideName,
            Address = g.Address,
            Phone = g.Phone,
            RecordBy = g.RecordBy,
            RecordTime = g.RecordTime
        };
    }
}
