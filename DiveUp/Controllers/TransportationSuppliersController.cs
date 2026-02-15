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
    public class TransportationSuppliersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TransportationSuppliersController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>Get all transportation suppliers - optional search by name</summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransportationSupplierDto>>> GetAll([FromQuery] string? search)
        {
            var query = _context.TransportationSuppliers.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                var s = search.Trim().ToLower();
                query = query.Where(x => x.SupplierName.ToLower().Contains(s));
            }

            var list = await query
                .OrderBy(s => s.SupplierName)
                .Select(s => new TransportationSupplierDto
                {
                    Id = s.Id,
                    SupplierName = s.SupplierName,
                    RecordBy = s.RecordBy,
                    RecordTime = s.RecordTime
                })
                .ToListAsync();

            return Ok(list);
        }

        /// <summary>Get transportation supplier by ID</summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<TransportationSupplierDto>> GetById(int id)
        {
            var s = await _context.TransportationSuppliers.FindAsync(id);
            if (s == null)
                return NotFound(new { message = $"Transportation Supplier with ID {id} not found." });

            return Ok(ToDto(s));
        }

        /// <summary>Create a new transportation supplier</summary>
        [HttpPost]
        public async Task<ActionResult<TransportationSupplierDto>> Create([FromBody] TransportationSupplierCreateDto dto)
        {
            var supplier = new TransportationSupplier
            {
                SupplierName = dto.SupplierName,
                RecordBy = dto.RecordBy,
                RecordTime = DateTime.Now
            };

            _context.TransportationSuppliers.Add(supplier);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = supplier.Id }, ToDto(supplier));
        }

        /// <summary>Update a transportation supplier</summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<TransportationSupplierDto>> Update(int id, [FromBody] TransportationSupplierUpdateDto dto)
        {
            var supplier = await _context.TransportationSuppliers.FindAsync(id);
            if (supplier == null)
                return NotFound(new { message = $"Transportation Supplier with ID {id} not found." });

            supplier.SupplierName = dto.SupplierName;
            supplier.RecordBy = dto.RecordBy;

            await _context.SaveChangesAsync();
            return Ok(ToDto(supplier));
        }

        /// <summary>Delete a transportation supplier</summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var supplier = await _context.TransportationSuppliers.FindAsync(id);
            if (supplier == null)
                return NotFound(new { message = $"Transportation Supplier with ID {id} not found." });

            _context.TransportationSuppliers.Remove(supplier);
            await _context.SaveChangesAsync();

            return Ok(new { message = $"Transportation Supplier '{supplier.SupplierName}' deleted successfully." });
        }

        private static TransportationSupplierDto ToDto(TransportationSupplier s) => new()
        {
            Id = s.Id,
            SupplierName = s.SupplierName,
            RecordBy = s.RecordBy,
            RecordTime = s.RecordTime
        };
    }
}
