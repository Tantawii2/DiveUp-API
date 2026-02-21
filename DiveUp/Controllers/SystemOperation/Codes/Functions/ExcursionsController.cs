using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DiveUp.Data;
using DiveUp.DTOs.SystemOperation.Codes.Functions;
using DiveUp.Models.SystemOperation.Codes.Functions;

namespace DiveUp.Controllers.SystemOperation.Codes.Functions
{
    [ApiController][Route("api/[controller]")][Produces("application/json")]
    public class ExcursionsController : ControllerBase
    {
        private readonly AppDbContext _db;
        public ExcursionsController(AppDbContext db) => _db = db;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExcursionDto>>> GetAll([FromQuery] string? search)
        {
            var q = _db.Excursions.Include(e=>e.Supplier).AsQueryable();
            if (!string.IsNullOrWhiteSpace(search)) { var s=search.Trim().ToLower(); q=q.Where(e=>e.ExcursionName.ToLower().Contains(s)); }
            return Ok(await q.OrderBy(e=>e.ExcursionName).Select(e=>ToDto(e)).ToListAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ExcursionDto>> GetById(int id)
        { var e=await _db.Excursions.Include(x=>x.Supplier).FirstOrDefaultAsync(x=>x.Id==id); return e==null?NotFound(new{message=$"Excursion {id} not found."}):Ok(ToDto(e)); }

        [HttpPost]
        public async Task<ActionResult<ExcursionDto>> Create([FromBody] ExcursionCreateDto dto)
        {
            var e=new Excursion{ExcursionName=dto.ExcursionName, SupplierId=dto.SupplierId, IsActive=dto.IsActive, RecordBy=dto.RecordBy, RecordTime=DateTime.UtcNow};
            _db.Excursions.Add(e); await _db.SaveChangesAsync();
            await _db.Entry(e).Reference(x=>x.Supplier).LoadAsync();
            return CreatedAtAction(nameof(GetById),new{id=e.Id},ToDto(e));
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ExcursionDto>> Update(int id, [FromBody] ExcursionUpdateDto dto)
        {
            var e=await _db.Excursions.Include(x=>x.Supplier).FirstOrDefaultAsync(x=>x.Id==id);
            if(e==null) return NotFound(new{message=$"Excursion {id} not found."});
            e.ExcursionName=dto.ExcursionName; e.SupplierId=dto.SupplierId; e.IsActive=dto.IsActive; e.RecordBy=dto.RecordBy;
            await _db.SaveChangesAsync(); await _db.Entry(e).Reference(x=>x.Supplier).LoadAsync();
            return Ok(ToDto(e));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        { var e=await _db.Excursions.FindAsync(id); if(e==null) return NotFound(new{message=$"Excursion {id} not found."}); _db.Excursions.Remove(e); await _db.SaveChangesAsync(); return Ok(new{message=$"'{e.ExcursionName}' deleted."}); }

        private static ExcursionDto ToDto(Excursion e) => new(){Id=e.Id,ExcursionName=e.ExcursionName,SupplierId=e.SupplierId,SupplierName=e.Supplier?.SupplierName,IsActive=e.IsActive,RecordBy=e.RecordBy,RecordTime=e.RecordTime};
    }
}
