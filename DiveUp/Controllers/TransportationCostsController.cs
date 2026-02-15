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
    public class TransportationCostsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TransportationCostsController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>Get all transportation costs - optional search by type name or currency</summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransportationCostDto>>> GetAll([FromQuery] string? search)
        {
            var query = _context.TransportationCosts.Include(tc => tc.Type).AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                var s = search.Trim().ToLower();
                query = query.Where(tc =>
                    tc.Currency.ToLower().Contains(s) ||
                    (tc.Type != null && tc.Type.TypeName.ToLower().Contains(s))
                );
            }

            var list = await query
                .OrderByDescending(tc => tc.RecordTime)
                .Select(tc => new TransportationCostDto
                {
                    Id = tc.Id,
                    TypeId = tc.TypeId,
                    TypeName = tc.Type != null ? tc.Type.TypeName : null,
                    CostValue = tc.CostValue,
                    Currency = tc.Currency,
                    FromDate = tc.FromDate,
                    ToDate = tc.ToDate,
                    RecordBy = tc.RecordBy,
                    RecordTime = tc.RecordTime
                })
                .ToListAsync();

            return Ok(list);
        }

        /// <summary>Get transportation cost by ID</summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<TransportationCostDto>> GetById(int id)
        {
            var tc = await _context.TransportationCosts.Include(x => x.Type).FirstOrDefaultAsync(x => x.Id == id);
            if (tc == null)
                return NotFound(new { message = $"Transportation Cost with ID {id} not found." });

            return Ok(ToDto(tc));
        }

        /// <summary>Create a new transportation cost</summary>
        [HttpPost]
        public async Task<ActionResult<TransportationCostDto>> Create([FromBody] TransportationCostCreateDto dto)
        {
            var cost = new TransportationCost
            {
                TypeId = dto.TypeId,
                CostValue = dto.CostValue,
                Currency = dto.Currency,
                FromDate = dto.FromDate,
                ToDate = dto.ToDate,
                RecordBy = dto.RecordBy,
                RecordTime = DateTime.Now
            };

            _context.TransportationCosts.Add(cost);
            await _context.SaveChangesAsync();
            await _context.Entry(cost).Reference(x => x.Type).LoadAsync();

            return CreatedAtAction(nameof(GetById), new { id = cost.Id }, ToDto(cost));
        }

        /// <summary>Update a transportation cost</summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<TransportationCostDto>> Update(int id, [FromBody] TransportationCostUpdateDto dto)
        {
            var cost = await _context.TransportationCosts.Include(x => x.Type).FirstOrDefaultAsync(x => x.Id == id);
            if (cost == null)
                return NotFound(new { message = $"Transportation Cost with ID {id} not found." });

            cost.TypeId = dto.TypeId;
            cost.CostValue = dto.CostValue;
            cost.Currency = dto.Currency;
            cost.FromDate = dto.FromDate;
            cost.ToDate = dto.ToDate;
            cost.RecordBy = dto.RecordBy;

            await _context.SaveChangesAsync();
            await _context.Entry(cost).Reference(x => x.Type).LoadAsync();

            return Ok(ToDto(cost));
        }

        /// <summary>Delete a transportation cost</summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var cost = await _context.TransportationCosts.FindAsync(id);
            if (cost == null)
                return NotFound(new { message = $"Transportation Cost with ID {id} not found." });

            _context.TransportationCosts.Remove(cost);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Transportation Cost deleted successfully." });
        }

        private static TransportationCostDto ToDto(TransportationCost tc) => new()
        {
            Id = tc.Id,
            TypeId = tc.TypeId,
            TypeName = tc.Type?.TypeName,
            CostValue = tc.CostValue,
            Currency = tc.Currency,
            FromDate = tc.FromDate,
            ToDate = tc.ToDate,
            RecordBy = tc.RecordBy,
            RecordTime = tc.RecordTime
        };
    }
}
