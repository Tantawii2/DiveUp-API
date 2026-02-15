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
    public class RatesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RatesController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>Get all rates</summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RateDto>>> GetAll()
        {
            var list = await _context.Rates
                .OrderByDescending(r => r.FromDate)
                .Select(r => new RateDto
                {
                    Id = r.Id,
                    FromDate = r.FromDate,
                    ToDate = r.ToDate,
                    Currency = r.Currency,
                    RateValue = r.RateValue,
                    RecordBy = r.RecordBy,
                    RecordTime = r.RecordTime
                })
                .ToListAsync();

            return Ok(list);
        }

        /// <summary>Get rate by ID</summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<RateDto>> GetById(int id)
        {
            var r = await _context.Rates.FindAsync(id);
            if (r == null)
                return NotFound(new { message = $"Rate with ID {id} not found." });

            return Ok(ToDto(r));
        }

        /// <summary>Create a new rate</summary>
        [HttpPost]
        public async Task<ActionResult<RateDto>> Create([FromBody] RateCreateDto dto)
        {
            var rate = new Rate
            {
                FromDate = dto.FromDate,
                ToDate = dto.ToDate,
                Currency = dto.Currency,
                RateValue = dto.RateValue,
                RecordBy = dto.RecordBy,
                RecordTime = DateTime.Now
            };

            _context.Rates.Add(rate);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = rate.Id }, ToDto(rate));
        }

        /// <summary>Update a rate</summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<RateDto>> Update(int id, [FromBody] RateUpdateDto dto)
        {
            var rate = await _context.Rates.FindAsync(id);
            if (rate == null)
                return NotFound(new { message = $"Rate with ID {id} not found." });

            rate.FromDate = dto.FromDate;
            rate.ToDate = dto.ToDate;
            rate.Currency = dto.Currency;
            rate.RateValue = dto.RateValue;
            rate.RecordBy = dto.RecordBy;

            await _context.SaveChangesAsync();
            return Ok(ToDto(rate));
        }

        /// <summary>Delete a rate</summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var rate = await _context.Rates.FindAsync(id);
            if (rate == null)
                return NotFound(new { message = $"Rate with ID {id} not found." });

            _context.Rates.Remove(rate);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Rate deleted successfully." });
        }

        private static RateDto ToDto(Rate r) => new()
        {
            Id = r.Id,
            FromDate = r.FromDate,
            ToDate = r.ToDate,
            Currency = r.Currency,
            RateValue = r.RateValue,
            RecordBy = r.RecordBy,
            RecordTime = r.RecordTime
        };
    }
}
