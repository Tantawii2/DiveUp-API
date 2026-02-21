using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DiveUp.Data;
using DiveUp.Models;
using DiveUp.DTOs;

namespace DiveUp.Controllers
{
    [ApiController][Route("api/[controller]")][Produces("application/json")]
    public class TransportationCostsController : ControllerBase
    {
        private readonly AppDbContext _db;
        public TransportationCostsController(AppDbContext db) => _db = db;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransportationCostDto>>> GetAll([FromQuery] string? search)
        {
            var q = _db.TransportationCosts.Include(tc=>tc.Supplier).Include(tc=>tc.CarType).Include(tc=>tc.Destination).AsQueryable();
            if (!string.IsNullOrWhiteSpace(search))
            {
                var s=search.Trim().ToLower();
                q=q.Where(tc=>tc.Supplier!=null&&tc.Supplier.SupplierName.ToLower().Contains(s)||tc.CarType!=null&&tc.CarType.TypeName.ToLower().Contains(s)||tc.Destination!=null&&tc.Destination.DestinationName.ToLower().Contains(s)||tc.RoundType.ToLower().Contains(s));
            }
            return Ok(await q.OrderByDescending(tc=>tc.RecordTime).Select(tc=>ToDto(tc)).ToListAsync());
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<TransportationCostDto>> GetById(int id)
        { var tc=await _db.TransportationCosts.Include(x=>x.Supplier).Include(x=>x.CarType).Include(x=>x.Destination).FirstOrDefaultAsync(x=>x.Id==id); return tc==null?NotFound(new{message=$"TransportationCost {id} not found."}):Ok(ToDto(tc)); }
        [HttpPost]
        public async Task<ActionResult<TransportationCostDto>> Create([FromBody] TransportationCostCreateDto dto)
        {
            var tc=new TransportationCost{SupplierId=dto.SupplierId,CarTypeId=dto.CarTypeId,DestinationId=dto.DestinationId,RoundType=dto.RoundType,CostEGP=dto.CostEGP,RecordBy=dto.RecordBy,RecordTime=DateTime.UtcNow};
            _db.TransportationCosts.Add(tc); await _db.SaveChangesAsync();
            await _db.Entry(tc).Reference(x=>x.Supplier).LoadAsync();
            await _db.Entry(tc).Reference(x=>x.CarType).LoadAsync();
            await _db.Entry(tc).Reference(x=>x.Destination).LoadAsync();
            return CreatedAtAction(nameof(GetById),new{id=tc.Id},ToDto(tc));
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult<TransportationCostDto>> Update(int id, [FromBody] TransportationCostUpdateDto dto)
        {
            var tc=await _db.TransportationCosts.Include(x=>x.Supplier).Include(x=>x.CarType).Include(x=>x.Destination).FirstOrDefaultAsync(x=>x.Id==id);
            if(tc==null) return NotFound(new{message=$"TransportationCost {id} not found."});
            tc.SupplierId=dto.SupplierId; tc.CarTypeId=dto.CarTypeId; tc.DestinationId=dto.DestinationId; tc.RoundType=dto.RoundType; tc.CostEGP=dto.CostEGP; tc.RecordBy=dto.RecordBy;
            await _db.SaveChangesAsync();
            await _db.Entry(tc).Reference(x=>x.Supplier).LoadAsync();
            await _db.Entry(tc).Reference(x=>x.CarType).LoadAsync();
            await _db.Entry(tc).Reference(x=>x.Destination).LoadAsync();
            return Ok(ToDto(tc));
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        { var tc=await _db.TransportationCosts.FindAsync(id); if(tc==null) return NotFound(new{message=$"TransportationCost {id} not found."}); _db.TransportationCosts.Remove(tc); await _db.SaveChangesAsync(); return Ok(new{message="TransportationCost deleted."}); }
        private static TransportationCostDto ToDto(TransportationCost tc) => new(){Id=tc.Id,SupplierId=tc.SupplierId,SupplierName=tc.Supplier?.SupplierName,CarTypeId=tc.CarTypeId,CarTypeName=tc.CarType?.TypeName,DestinationId=tc.DestinationId,DestinationName=tc.Destination?.DestinationName,RoundType=tc.RoundType,CostEGP=tc.CostEGP,RecordBy=tc.RecordBy,RecordTime=tc.RecordTime};
    }
}
