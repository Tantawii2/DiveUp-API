using Microsoft.AspNetCore.Mvc; using Microsoft.EntityFrameworkCore;
using DiveUp.Data; using DiveUp.DTOs; using DiveUp.Models;
namespace DiveUp.Controllers
{
    [ApiController][Route("api/[controller]")][Produces("application/json")]
    public class RepVouchersController : ControllerBase
    {
        private readonly AppDbContext _db;
        public RepVouchersController(AppDbContext db) => _db = db;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RepVoucherDto>>> GetAll([FromQuery] string? search)
        {
            var q = _db.RepVouchers.Include(v=>v.Rep).AsQueryable();
            if (!string.IsNullOrWhiteSpace(search)) { var s=search.Trim().ToLower(); q=q.Where(v=>v.Rep!=null&&v.Rep.RepName.ToLower().Contains(s)); }
            return Ok(await q.OrderByDescending(v=>v.RecordTime).Select(v=>ToDto(v)).ToListAsync());
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<RepVoucherDto>> GetById(int id)
        { var v=await _db.RepVouchers.Include(x=>x.Rep).FirstOrDefaultAsync(x=>x.Id==id); return v==null?NotFound(new{message=$"RepVoucher {id} not found."}):Ok(ToDto(v)); }
        [HttpPost]
        public async Task<ActionResult<RepVoucherDto>> Create([FromBody] RepVoucherCreateDto dto)
        {
            if(dto.ToNumber<dto.FromNumber) return BadRequest(new{message="ToNumber must be >= FromNumber."});
            var v=new RepVoucher{RepId=dto.RepId,FromNumber=dto.FromNumber,ToNumber=dto.ToNumber,CountVouchers=dto.ToNumber-dto.FromNumber+1,RecordBy=dto.RecordBy,RecordTime=DateTime.UtcNow};
            _db.RepVouchers.Add(v); await _db.SaveChangesAsync(); await _db.Entry(v).Reference(x=>x.Rep).LoadAsync();
            return CreatedAtAction(nameof(GetById),new{id=v.Id},ToDto(v));
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult<RepVoucherDto>> Update(int id, [FromBody] RepVoucherUpdateDto dto)
        {
            if(dto.ToNumber<dto.FromNumber) return BadRequest(new{message="ToNumber must be >= FromNumber."});
            var v=await _db.RepVouchers.Include(x=>x.Rep).FirstOrDefaultAsync(x=>x.Id==id); if(v==null) return NotFound(new{message=$"RepVoucher {id} not found."});
            v.RepId=dto.RepId; v.FromNumber=dto.FromNumber; v.ToNumber=dto.ToNumber; v.CountVouchers=dto.ToNumber-dto.FromNumber+1; v.RecordBy=dto.RecordBy;
            await _db.SaveChangesAsync(); await _db.Entry(v).Reference(x=>x.Rep).LoadAsync(); return Ok(ToDto(v));
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        { var v=await _db.RepVouchers.FindAsync(id); if(v==null) return NotFound(new{message=$"RepVoucher {id} not found."}); _db.RepVouchers.Remove(v); await _db.SaveChangesAsync(); return Ok(new{message="RepVoucher deleted."}); }
        private static RepVoucherDto ToDto(RepVoucher v) => new(){Id=v.Id,RepId=v.RepId,RepName=v.Rep?.RepName,FromNumber=v.FromNumber,ToNumber=v.ToNumber,CountVouchers=v.CountVouchers,RecordBy=v.RecordBy,RecordTime=v.RecordTime};
    }
}
