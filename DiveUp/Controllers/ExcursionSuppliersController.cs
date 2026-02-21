using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DiveUp.Data;
using DiveUp.Models;
using DiveUp.DTOs;

namespace DiveUp.Controllers
{
    [ApiController][Route("api/[controller]")][Produces("application/json")]
    public class ExcursionSuppliersController : ControllerBase
    {
        private readonly AppDbContext _db;
        public ExcursionSuppliersController(AppDbContext db) => _db = db;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExcursionSupplierDto>>> GetAll([FromQuery] string? search)
        {
            var q = _db.ExcursionSuppliers.AsQueryable();
            if (!string.IsNullOrWhiteSpace(search)) { var s = search.Trim().ToLower(); q = q.Where(x => x.SupplierName.ToLower().Contains(s)); }
            return Ok(await q.OrderBy(x => x.SupplierName).Select(x => ToDto(x)).ToListAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ExcursionSupplierDto>> GetById(int id)
        { var x = await _db.ExcursionSuppliers.FindAsync(id); return x == null ? NotFound(new{message=$"ExcursionSupplier {id} not found."}) : Ok(ToDto(x)); }

        [HttpPost]
        public async Task<ActionResult<ExcursionSupplierDto>> Create([FromBody] ExcursionSupplierCreateDto dto)
        {
            var s = new ExcursionSupplier { SupplierName=dto.SupplierName, VatNo=dto.VatNo, FileNo=dto.FileNo, Email=dto.Email, Address=dto.Address, Phone=dto.Phone, IsActive=dto.IsActive, RecordBy=dto.RecordBy, RecordTime=DateTime.UtcNow };
            _db.ExcursionSuppliers.Add(s); await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new{id=s.Id}, ToDto(s));
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ExcursionSupplierDto>> Update(int id, [FromBody] ExcursionSupplierUpdateDto dto)
        {
            var s = await _db.ExcursionSuppliers.FindAsync(id);
            if (s == null) return NotFound(new{message=$"ExcursionSupplier {id} not found."});
            s.SupplierName=dto.SupplierName; s.VatNo=dto.VatNo; s.FileNo=dto.FileNo; s.Email=dto.Email; s.Address=dto.Address; s.Phone=dto.Phone; s.IsActive=dto.IsActive; s.RecordBy=dto.RecordBy;
            await _db.SaveChangesAsync(); return Ok(ToDto(s));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        { var s = await _db.ExcursionSuppliers.FindAsync(id); if(s==null) return NotFound(new{message=$"ExcursionSupplier {id} not found."}); _db.ExcursionSuppliers.Remove(s); await _db.SaveChangesAsync(); return Ok(new{message=$"'{s.SupplierName}' deleted."}); }

        private static ExcursionSupplierDto ToDto(ExcursionSupplier x) => new() { Id=x.Id, SupplierName=x.SupplierName, VatNo=x.VatNo, FileNo=x.FileNo, Email=x.Email, Address=x.Address, Phone=x.Phone, IsActive=x.IsActive, RecordBy=x.RecordBy, RecordTime=x.RecordTime };
    }
}
