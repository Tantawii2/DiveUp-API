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
    public class ExcursionSuppliersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ExcursionSuppliersController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>Get all excursion suppliers - optional search by name</summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExcursionSupplierDto>>> GetAll([FromQuery] string? search)
        {
            var query = _context.ExcursionSuppliers.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                var s = search.Trim().ToLower();
                query = query.Where(x => x.SupplierName.ToLower().Contains(s));
            }

            var list = await query
                .OrderBy(s => s.SupplierName)
                .Select(s => new ExcursionSupplierDto
                {
                    Id = s.Id,
                    SupplierName = s.SupplierName,
                    RecordBy = s.RecordBy,
                    RecordTime = s.RecordTime
                })
                .ToListAsync();

            return Ok(list);
        }

        /// <summary>Get excursion supplier by ID</summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<ExcursionSupplierDto>> GetById(int id)
        {
            var s = await _context.ExcursionSuppliers.FindAsync(id);
            if (s == null)
                return NotFound(new { message = $"Excursion Supplier with ID {id} not found." });

            return Ok(ToDto(s));
        }

        /// <summary>Create a new excursion supplier</summary>
        [HttpPost]
        public async Task<ActionResult<ExcursionSupplierDto>> Create([FromBody] ExcursionSupplierCreateDto dto)
        {
            var supplier = new ExcursionSupplier
            {
                SupplierName = dto.SupplierName,
                RecordBy = dto.RecordBy,
                RecordTime = DateTime.Now
            };

            _context.ExcursionSuppliers.Add(supplier);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = supplier.Id }, ToDto(supplier));
        }

        /// <summary>Update an excursion supplier</summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<ExcursionSupplierDto>> Update(int id, [FromBody] ExcursionSupplierUpdateDto dto)
        {
            var supplier = await _context.ExcursionSuppliers.FindAsync(id);
            if (supplier == null)
                return NotFound(new { message = $"Excursion Supplier with ID {id} not found." });

            supplier.SupplierName = dto.SupplierName;
            supplier.RecordBy = dto.RecordBy;

            await _context.SaveChangesAsync();
            return Ok(ToDto(supplier));
        }

        /// <summary>Delete an excursion supplier</summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var supplier = await _context.ExcursionSuppliers.FindAsync(id);
            if (supplier == null)
                return NotFound(new { message = $"Excursion Supplier with ID {id} not found." });

            _context.ExcursionSuppliers.Remove(supplier);
            await _context.SaveChangesAsync();

            return Ok(new { message = $"Excursion Supplier '{supplier.SupplierName}' deleted successfully." });
        }

        private static ExcursionSupplierDto ToDto(ExcursionSupplier s) => new()
        {
            Id = s.Id,
            SupplierName = s.SupplierName,
            RecordBy = s.RecordBy,
            RecordTime = s.RecordTime
        };
    }
}
