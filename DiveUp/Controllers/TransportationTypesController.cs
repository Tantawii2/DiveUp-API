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
    public class TransportationTypesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TransportationTypesController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>Get all transportation types - optional search by type name or supplier</summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransportationTypeDto>>> GetAll([FromQuery] string? search)
        {
            var query = _context.TransportationTypes.Include(t => t.Supplier).AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                var s = search.Trim().ToLower();
                query = query.Where(t =>
                    t.TypeName.ToLower().Contains(s) ||
                    (t.Supplier != null && t.Supplier.SupplierName.ToLower().Contains(s))
                );
            }

            var list = await query
                .OrderBy(t => t.TypeName)
                .Select(t => new TransportationTypeDto
                {
                    Id = t.Id,
                    TypeName = t.TypeName,
                    SupplierId = t.SupplierId,
                    SupplierName = t.Supplier != null ? t.Supplier.SupplierName : null,
                    RecordBy = t.RecordBy,
                    RecordTime = t.RecordTime
                })
                .ToListAsync();

            return Ok(list);
        }

        /// <summary>Get transportation type by ID</summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<TransportationTypeDto>> GetById(int id)
        {
            var t = await _context.TransportationTypes.Include(x => x.Supplier).FirstOrDefaultAsync(x => x.Id == id);
            if (t == null)
                return NotFound(new { message = $"Transportation Type with ID {id} not found." });

            return Ok(ToDto(t));
        }

        /// <summary>Create a new transportation type</summary>
        [HttpPost]
        public async Task<ActionResult<TransportationTypeDto>> Create([FromBody] TransportationTypeCreateDto dto)
        {
            var type = new TransportationType
            {
                TypeName = dto.TypeName,
                SupplierId = dto.SupplierId,
                RecordBy = dto.RecordBy,
                RecordTime = DateTime.Now
            };

            _context.TransportationTypes.Add(type);
            await _context.SaveChangesAsync();
            await _context.Entry(type).Reference(x => x.Supplier).LoadAsync();

            return CreatedAtAction(nameof(GetById), new { id = type.Id }, ToDto(type));
        }

        /// <summary>Update a transportation type</summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<TransportationTypeDto>> Update(int id, [FromBody] TransportationTypeUpdateDto dto)
        {
            var type = await _context.TransportationTypes.Include(x => x.Supplier).FirstOrDefaultAsync(x => x.Id == id);
            if (type == null)
                return NotFound(new { message = $"Transportation Type with ID {id} not found." });

            type.TypeName = dto.TypeName;
            type.SupplierId = dto.SupplierId;
            type.RecordBy = dto.RecordBy;

            await _context.SaveChangesAsync();
            await _context.Entry(type).Reference(x => x.Supplier).LoadAsync();

            return Ok(ToDto(type));
        }

        /// <summary>Delete a transportation type</summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var type = await _context.TransportationTypes.FindAsync(id);
            if (type == null)
                return NotFound(new { message = $"Transportation Type with ID {id} not found." });

            _context.TransportationTypes.Remove(type);
            await _context.SaveChangesAsync();

            return Ok(new { message = $"Transportation Type '{type.TypeName}' deleted successfully." });
        }

        private static TransportationTypeDto ToDto(TransportationType t) => new()
        {
            Id = t.Id,
            TypeName = t.TypeName,
            SupplierId = t.SupplierId,
            SupplierName = t.Supplier?.SupplierName,
            RecordBy = t.RecordBy,
            RecordTime = t.RecordTime
        };
    }
}
