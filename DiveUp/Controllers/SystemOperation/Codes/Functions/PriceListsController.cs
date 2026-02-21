using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DiveUp.Data;
using DiveUp.DTOs.SystemOperation.Codes.Functions;
using DiveUp.Models.SystemOperation.Codes.Functions;

namespace DiveUp.Controllers.SystemOperation.Codes.Functions
{
    [ApiController][Route("api/[controller]")][Produces("application/json")]
    public class PriceListsController : ControllerBase
    {
        private readonly AppDbContext _db;
        public PriceListsController(AppDbContext db) => _db = db;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PriceListDto>>> GetAll([FromQuery] string? search)
        {
            var q = _db.PriceLists.AsQueryable();
            if (!string.IsNullOrWhiteSpace(search)) { var s=search.Trim().ToLower(); q=q.Where(p=>p.PriceListName.ToLower().Contains(s)); }
            return Ok(await q.OrderBy(p=>p.PriceListName).Select(p=>ToDto(p)).ToListAsync());
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<PriceListDto>> GetById(int id)
        { var p=await _db.PriceLists.FindAsync(id); return p==null?NotFound(new{message=$"PriceList {id} not found."}):Ok(ToDto(p)); }
        [HttpPost]
        public async Task<ActionResult<PriceListDto>> Create([FromBody] PriceListCreateDto dto)
        { var p=new PriceList{PriceListName=dto.PriceListName,IsActive=dto.IsActive,RecordBy=dto.RecordBy,RecordTime=DateTime.UtcNow}; _db.PriceLists.Add(p); await _db.SaveChangesAsync(); return CreatedAtAction(nameof(GetById),new{id=p.Id},ToDto(p)); }
        [HttpPut("{id:int}")]
        public async Task<ActionResult<PriceListDto>> Update(int id, [FromBody] PriceListUpdateDto dto)
        { var p=await _db.PriceLists.FindAsync(id); if(p==null) return NotFound(new{message=$"PriceList {id} not found."}); p.PriceListName=dto.PriceListName; p.IsActive=dto.IsActive; p.RecordBy=dto.RecordBy; await _db.SaveChangesAsync(); return Ok(ToDto(p)); }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        { var p=await _db.PriceLists.FindAsync(id); if(p==null) return NotFound(new{message=$"PriceList {id} not found."}); _db.PriceLists.Remove(p); await _db.SaveChangesAsync(); return Ok(new{message=$"'{p.PriceListName}' deleted."}); }
        private static PriceListDto ToDto(PriceList p) => new(){Id=p.Id,PriceListName=p.PriceListName,IsActive=p.IsActive,RecordBy=p.RecordBy,RecordTime=p.RecordTime};
    }
}
