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
    public class BoatsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BoatsController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>Get all boats</summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BoatDto>>> GetAll()
        {
            var list = await _context.Boats
                .OrderBy(b => b.BoatName)
                .Select(b => new BoatDto
                {
                    Id = b.Id,
                    BoatName = b.BoatName,
                    Capacity = b.Capacity,
                    Status = b.Status,
                    RecordBy = b.RecordBy,
                    RecordTime = b.RecordTime
                })
                .ToListAsync();

            return Ok(list);
        }

        /// <summary>Get boat by ID</summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<BoatDto>> GetById(int id)
        {
            var b = await _context.Boats.FindAsync(id);
            if (b == null)
                return NotFound(new { message = $"Boat with ID {id} not found." });

            return Ok(ToDto(b));
        }

        /// <summary>Create a new boat</summary>
        [HttpPost]
        public async Task<ActionResult<BoatDto>> Create([FromBody] BoatCreateDto dto)
        {
            var boat = new Boat
            {
                BoatName = dto.BoatName,
                Capacity = dto.Capacity,
                Status = dto.Status,
                RecordBy = dto.RecordBy,
                RecordTime = DateTime.Now
            };

            _context.Boats.Add(boat);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = boat.Id }, ToDto(boat));
        }

        /// <summary>Update a boat</summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<BoatDto>> Update(int id, [FromBody] BoatUpdateDto dto)
        {
            var boat = await _context.Boats.FindAsync(id);
            if (boat == null)
                return NotFound(new { message = $"Boat with ID {id} not found." });

            boat.BoatName = dto.BoatName;
            boat.Capacity = dto.Capacity;
            boat.Status = dto.Status;
            boat.RecordBy = dto.RecordBy;

            await _context.SaveChangesAsync();
            return Ok(ToDto(boat));
        }

        /// <summary>Delete a boat</summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var boat = await _context.Boats.FindAsync(id);
            if (boat == null)
                return NotFound(new { message = $"Boat with ID {id} not found." });

            _context.Boats.Remove(boat);
            await _context.SaveChangesAsync();

            return Ok(new { message = $"Boat '{boat.BoatName}' deleted successfully." });
        }

        private static BoatDto ToDto(Boat b) => new()
        {
            Id = b.Id,
            BoatName = b.BoatName,
            Capacity = b.Capacity,
            Status = b.Status,
            RecordBy = b.RecordBy,
            RecordTime = b.RecordTime
        };
    }
}
