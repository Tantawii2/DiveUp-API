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
    public class ExcursionsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ExcursionsController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>Get all excursions</summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExcursionDto>>> GetAll()
        {
            var list = await _context.Excursions
                .Include(e => e.Supplier)
                .OrderBy(e => e.ExcursionName)
                .Select(e => new ExcursionDto
                {
                    Id = e.Id,
                    ExcursionName = e.ExcursionName,
                    SupplierId = e.SupplierId,
                    SupplierName = e.Supplier != null ? e.Supplier.SupplierName : null,
                    RecordBy = e.RecordBy,
                    RecordTime = e.RecordTime
                })
                .ToListAsync();

            return Ok(list);
        }

        /// <summary>Get excursion by ID</summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<ExcursionDto>> GetById(int id)
        {
            var e = await _context.Excursions
                .Include(x => x.Supplier)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (e == null)
                return NotFound(new { message = $"Excursion with ID {id} not found." });

            return Ok(ToDto(e));
        }

        /// <summary>Create a new excursion</summary>
        [HttpPost]
        public async Task<ActionResult<ExcursionDto>> Create([FromBody] ExcursionCreateDto dto)
        {
            var excursion = new Excursion
            {
                ExcursionName = dto.ExcursionName,
                SupplierId = dto.SupplierId,
                RecordBy = dto.RecordBy,
                RecordTime = DateTime.Now
            };

            _context.Excursions.Add(excursion);
            await _context.SaveChangesAsync();
            await _context.Entry(excursion).Reference(x => x.Supplier).LoadAsync();

            return CreatedAtAction(nameof(GetById), new { id = excursion.Id }, ToDto(excursion));
        }

        /// <summary>Update an excursion</summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<ExcursionDto>> Update(int id, [FromBody] ExcursionUpdateDto dto)
        {
            var excursion = await _context.Excursions
                .Include(x => x.Supplier)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (excursion == null)
                return NotFound(new { message = $"Excursion with ID {id} not found." });

            excursion.ExcursionName = dto.ExcursionName;
            excursion.SupplierId = dto.SupplierId;
            excursion.RecordBy = dto.RecordBy;

            await _context.SaveChangesAsync();
            await _context.Entry(excursion).Reference(x => x.Supplier).LoadAsync();

            return Ok(ToDto(excursion));
        }

        /// <summary>Delete an excursion</summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var excursion = await _context.Excursions.FindAsync(id);
            if (excursion == null)
                return NotFound(new { message = $"Excursion with ID {id} not found." });

            _context.Excursions.Remove(excursion);
            await _context.SaveChangesAsync();

            return Ok(new { message = $"Excursion '{excursion.ExcursionName}' deleted successfully." });
        }

        private static ExcursionDto ToDto(Excursion e) => new()
        {
            Id = e.Id,
            ExcursionName = e.ExcursionName,
            SupplierId = e.SupplierId,
            SupplierName = e.Supplier?.SupplierName,
            RecordBy = e.RecordBy,
            RecordTime = e.RecordTime
        };
    }
}
