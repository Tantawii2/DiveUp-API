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
    public class NationalitiesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public NationalitiesController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>Get all nationalities - optional search by name</summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NationalityDto>>> GetAll([FromQuery] string? search)
        {
            var query = _context.Nationalities.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                var s = search.Trim().ToLower();
                query = query.Where(n => n.NationalityName.ToLower().Contains(s));
            }

            var list = await query
                .OrderBy(n => n.NationalityName)
                .Select(n => new NationalityDto
                {
                    Id = n.Id,
                    NationalityName = n.NationalityName,
                    RecordBy = n.RecordBy,
                    RecordTime = n.RecordTime
                })
                .ToListAsync();

            return Ok(list);
        }

        /// <summary>Get nationality by ID</summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<NationalityDto>> GetById(int id)
        {
            var n = await _context.Nationalities.FindAsync(id);
            if (n == null)
                return NotFound(new { message = $"Nationality with ID {id} not found." });

            return Ok(ToDto(n));
        }

        /// <summary>Create a new nationality</summary>
        [HttpPost]
        public async Task<ActionResult<NationalityDto>> Create([FromBody] NationalityCreateDto dto)
        {
            var nationality = new Nationality
            {
                NationalityName = dto.NationalityName,
                RecordBy = dto.RecordBy,
                RecordTime = DateTime.Now
            };

            _context.Nationalities.Add(nationality);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = nationality.Id }, ToDto(nationality));
        }

        /// <summary>Update a nationality</summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<NationalityDto>> Update(int id, [FromBody] NationalityUpdateDto dto)
        {
            var nationality = await _context.Nationalities.FindAsync(id);
            if (nationality == null)
                return NotFound(new { message = $"Nationality with ID {id} not found." });

            nationality.NationalityName = dto.NationalityName;
            nationality.RecordBy = dto.RecordBy;

            await _context.SaveChangesAsync();
            return Ok(ToDto(nationality));
        }

        /// <summary>Delete a nationality</summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var nationality = await _context.Nationalities.FindAsync(id);
            if (nationality == null)
                return NotFound(new { message = $"Nationality with ID {id} not found." });

            _context.Nationalities.Remove(nationality);
            await _context.SaveChangesAsync();

            return Ok(new { message = $"Nationality '{nationality.NationalityName}' deleted successfully." });
        }

        private static NationalityDto ToDto(Nationality n) => new()
        {
            Id = n.Id,
            NationalityName = n.NationalityName,
            RecordBy = n.RecordBy,
            RecordTime = n.RecordTime
        };
    }
}
