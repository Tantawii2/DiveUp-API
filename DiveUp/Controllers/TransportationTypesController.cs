using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DiveUp.Data;
using DiveUp.Models;
using DiveUp.DTOs;

namespace DiveUp.Controllers
{
    [ApiController][Route("api/[controller]")][Produces("application/json")]
    public class TransportationTypesController : ControllerBase
    {
        private readonly AppDbContext _db;
        public TransportationTypesController(AppDbContext db) => _db = db;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransportationTypeDto>>> GetAll([FromQuery] string? search)
        {
            var q = _db.TransportationTypes.Include(t=>t.Supplier).AsQueryable();
            if (!string.IsNullOrWhiteSpace(search)) { var s=search.Trim().ToLower(); q=q.Where(t=>t.TypeName.ToLower().Contains(s)||t.Supplier!=null&&t.Supplier.SupplierName.ToLower().Contains(s)); }
            return Ok(await q.OrderBy(t=>t.TypeName).Select(t=>ToDto(t)).ToListAsync());
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<TransportationTypeDto>> GetById(int id)
        { var t=await _db.TransportationTypes.Include(x=>x.Supplier).FirstOrDefaultAsync(x=>x.Id==id); return t==null?NotFound(new{message=$"TransportationType {id} not found."}):Ok(ToDto(t)); }
        [HttpPost]
        public async Task<ActionResult<TransportationTypeDto>> Create([FromBody] TransportationTypeCreateDto dto)
        {
            var t=new TransportationType{TypeName=dto.TypeName,SupplierId=dto.SupplierId,IsActive=dto.IsActive,RecordBy=dto.RecordBy,RecordTime=DateTime.UtcNow};
            _db.TransportationTypes.Add(t); await _db.SaveChangesAsync(); await _db.Entry(t).Reference(x=>x.Supplier).LoadAsync();
            return CreatedAtAction(nameof(GetById),new{id=t.Id},ToDto(t));
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult<TransportationTypeDto>> Update(int id, [FromBody] TransportationTypeUpdateDto dto)
        {
            var t=await _db.TransportationTypes.Include(x=>x.Supplier).FirstOrDefaultAsync(x=>x.Id==id); if(t==null) return NotFound(new{message=$"TransportationType {id} not found."});
            t.TypeName=dto.TypeName; t.SupplierId=dto.SupplierId; t.IsActive=dto.IsActive; t.RecordBy=dto.RecordBy;
            await _db.SaveChangesAsync(); await _db.Entry(t).Reference(x=>x.Supplier).LoadAsync(); return Ok(ToDto(t));
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        { var t=await _db.TransportationTypes.FindAsync(id); if(t==null) return NotFound(new{message=$"TransportationType {id} not found."}); _db.TransportationTypes.Remove(t); await _db.SaveChangesAsync(); return Ok(new{message=$"'{t.TypeName}' deleted."}); }
        private static TransportationTypeDto ToDto(TransportationType t) => new(){Id=t.Id,TypeName=t.TypeName,SupplierId=t.SupplierId,SupplierName=t.Supplier?.SupplierName,IsActive=t.IsActive,RecordBy=t.RecordBy,RecordTime=t.RecordTime};
    }
}
