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
    public class HotelsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public HotelsController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>Get all hotels</summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HotelDto>>> GetAll()
        {
            var list = await _context.Hotels
                .Include(h => h.Destination)
                .OrderBy(h => h.HotelName)
                .Select(h => new HotelDto
                {
                    Id = h.Id,
                    HotelName = h.HotelName,
                    DestinationId = h.DestinationId,
                    DestinationName = h.Destination != null ? h.Destination.DestinationName : null,
                    RecordBy = h.RecordBy,
                    RecordTime = h.RecordTime
                })
                .ToListAsync();

            return Ok(list);
        }

        /// <summary>Get hotel by ID</summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<HotelDto>> GetById(int id)
        {
            var h = await _context.Hotels
                .Include(x => x.Destination)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (h == null)
                return NotFound(new { message = $"Hotel with ID {id} not found." });

            return Ok(ToDto(h));
        }

        /// <summary>Create a new hotel</summary>
        [HttpPost]
        public async Task<ActionResult<HotelDto>> Create([FromBody] HotelCreateDto dto)
        {
            var hotel = new Hotel
            {
                HotelName = dto.HotelName,
                DestinationId = dto.DestinationId,
                RecordBy = dto.RecordBy,
                RecordTime = DateTime.Now
            };

            _context.Hotels.Add(hotel);
            await _context.SaveChangesAsync();
            await _context.Entry(hotel).Reference(x => x.Destination).LoadAsync();

            return CreatedAtAction(nameof(GetById), new { id = hotel.Id }, ToDto(hotel));
        }

        /// <summary>Update a hotel</summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<HotelDto>> Update(int id, [FromBody] HotelUpdateDto dto)
        {
            var hotel = await _context.Hotels
                .Include(x => x.Destination)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (hotel == null)
                return NotFound(new { message = $"Hotel with ID {id} not found." });

            hotel.HotelName = dto.HotelName;
            hotel.DestinationId = dto.DestinationId;
            hotel.RecordBy = dto.RecordBy;

            await _context.SaveChangesAsync();
            await _context.Entry(hotel).Reference(x => x.Destination).LoadAsync();

            return Ok(ToDto(hotel));
        }

        /// <summary>Delete a hotel</summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var hotel = await _context.Hotels.FindAsync(id);
            if (hotel == null)
                return NotFound(new { message = $"Hotel with ID {id} not found." });

            _context.Hotels.Remove(hotel);
            await _context.SaveChangesAsync();

            return Ok(new { message = $"Hotel '{hotel.HotelName}' deleted successfully." });
        }

        private static HotelDto ToDto(Hotel h) => new()
        {
            Id = h.Id,
            HotelName = h.HotelName,
            DestinationId = h.DestinationId,
            DestinationName = h.Destination?.DestinationName,
            RecordBy = h.RecordBy,
            RecordTime = h.RecordTime
        };
    }
}
