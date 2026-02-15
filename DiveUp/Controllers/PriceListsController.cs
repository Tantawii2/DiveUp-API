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
    public class PriceListsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PriceListsController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>Get all price lists - optional search by name</summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PriceListDto>>> GetAll([FromQuery] string? search)
        {
            var query = _context.PriceLists.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                var s = search.Trim().ToLower();
                query = query.Where(p => p.PriceListName.ToLower().Contains(s));
            }

            var list = await query
                .OrderBy(p => p.PriceListName)
                .Select(p => new PriceListDto
                {
                    Id = p.Id,
                    PriceListName = p.PriceListName,
                    RecordBy = p.RecordBy,
                    RecordTime = p.RecordTime
                })
                .ToListAsync();

            return Ok(list);
        }

        /// <summary>Get price list by ID</summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<PriceListDto>> GetById(int id)
        {
            var p = await _context.PriceLists.FindAsync(id);
            if (p == null)
                return NotFound(new { message = $"Price List with ID {id} not found." });

            return Ok(ToDto(p));
        }

        /// <summary>Create a new price list</summary>
        [HttpPost]
        public async Task<ActionResult<PriceListDto>> Create([FromBody] PriceListCreateDto dto)
        {
            var priceList = new PriceList
            {
                PriceListName = dto.PriceListName,
                RecordBy = dto.RecordBy,
                RecordTime = DateTime.Now
            };

            _context.PriceLists.Add(priceList);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = priceList.Id }, ToDto(priceList));
        }

        /// <summary>Update a price list</summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<PriceListDto>> Update(int id, [FromBody] PriceListUpdateDto dto)
        {
            var priceList = await _context.PriceLists.FindAsync(id);
            if (priceList == null)
                return NotFound(new { message = $"Price List with ID {id} not found." });

            priceList.PriceListName = dto.PriceListName;
            priceList.RecordBy = dto.RecordBy;

            await _context.SaveChangesAsync();
            return Ok(ToDto(priceList));
        }

        /// <summary>Delete a price list</summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var priceList = await _context.PriceLists.FindAsync(id);
            if (priceList == null)
                return NotFound(new { message = $"Price List with ID {id} not found." });

            _context.PriceLists.Remove(priceList);
            await _context.SaveChangesAsync();

            return Ok(new { message = $"Price List '{priceList.PriceListName}' deleted successfully." });
        }

        private static PriceListDto ToDto(PriceList p) => new()
        {
            Id = p.Id,
            PriceListName = p.PriceListName,
            RecordBy = p.RecordBy,
            RecordTime = p.RecordTime
        };
    }
}
