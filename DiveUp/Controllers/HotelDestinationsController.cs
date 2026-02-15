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
    public class HotelDestinationsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public HotelDestinationsController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>Get all hotel destinations</summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HotelDestinationDto>>> GetAll()
        {
            var list = await _context.HotelDestinations
                .OrderBy(d => d.DestinationName)
                .Select(d => new HotelDestinationDto
                {
                    Id = d.Id,
                    DestinationName = d.DestinationName,
                    RecordBy = d.RecordBy,
                    RecordTime = d.RecordTime
                })
                .ToListAsync();

            return Ok(list);
        }

        /// <summary>Get hotel destination by ID</summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<HotelDestinationDto>> GetById(int id)
        {
            var d = await _context.HotelDestinations.FindAsync(id);
            if (d == null)
                return NotFound(new { message = $"Hotel Destination with ID {id} not found." });

            return Ok(ToDto(d));
        }

        /// <summary>Create a new hotel destination</summary>
        [HttpPost]
        public async Task<ActionResult<HotelDestinationDto>> Create([FromBody] HotelDestinationCreateDto dto)
        {
            var dest = new HotelDestination
            {
                DestinationName = dto.DestinationName,
                RecordBy = dto.RecordBy,
                RecordTime = DateTime.Now
            };

            _context.HotelDestinations.Add(dest);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = dest.Id }, ToDto(dest));
        }

        /// <summary>Update a hotel destination</summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<HotelDestinationDto>> Update(int id, [FromBody] HotelDestinationUpdateDto dto)
        {
            var dest = await _context.HotelDestinations.FindAsync(id);
            if (dest == null)
                return NotFound(new { message = $"Hotel Destination with ID {id} not found." });

            dest.DestinationName = dto.DestinationName;
            dest.RecordBy = dto.RecordBy;

            await _context.SaveChangesAsync();
            return Ok(ToDto(dest));
        }

        /// <summary>Delete a hotel destination</summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var dest = await _context.HotelDestinations.FindAsync(id);
            if (dest == null)
                return NotFound(new { message = $"Hotel Destination with ID {id} not found." });

            _context.HotelDestinations.Remove(dest);
            await _context.SaveChangesAsync();

            return Ok(new { message = $"Hotel Destination '{dest.DestinationName}' deleted successfully." });
        }

        private static HotelDestinationDto ToDto(HotelDestination d) => new()
        {
            Id = d.Id,
            DestinationName = d.DestinationName,
            RecordBy = d.RecordBy,
            RecordTime = d.RecordTime
        };
    }
}
