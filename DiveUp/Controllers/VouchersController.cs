using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DiveUp.Data;
using DiveUp.Models;
using DiveUp.DTOs;

namespace DiveUp.Controllers
{
    [ApiController][Route("api/[controller]")][Produces("application/json")]
    public class VouchersController : ControllerBase
    {
        private readonly AppDbContext _db;
        public VouchersController(AppDbContext db) => _db = db;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VoucherDto>>> GetAll([FromQuery] string? search)
        {
            var q = _db.Vouchers.Include(v=>v.Rep).AsQueryable();
            if (!string.IsNullOrWhiteSpace(search)) { var s=search.Trim().ToLower(); q=q.Where(v=>v.Rep!=null&&v.Rep.RepName.ToLower().Contains(s)); }
            return Ok(await q.OrderByDescending(v=>v.RecordTime).Select(v=>ToDto(v)).ToListAsync());
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<VoucherDto>> GetById(int id)
        { var v=await _db.Vouchers.Include(x=>x.Rep).FirstOrDefaultAsync(x=>x.Id==id); return v==null?NotFound(new{message=$"Voucher {id} not found."}):Ok(ToDto(v)); }
        [HttpPost]
        public async Task<ActionResult<VoucherDto>> Create([FromBody] VoucherCreateDto dto)
        {
            if(dto.ToNumber<dto.FromNumber) return BadRequest(new{message="ToNumber must be >= FromNumber."});
            var v=new Voucher{RepId=dto.RepId,FromNumber=dto.FromNumber,ToNumber=dto.ToNumber,CountVouchers=dto.ToNumber-dto.FromNumber+1,RecordBy=dto.RecordBy,RecordTime=DateTime.UtcNow};
            _db.Vouchers.Add(v); await _db.SaveChangesAsync(); await _db.Entry(v).Reference(x=>x.Rep).LoadAsync();
            return CreatedAtAction(nameof(GetById),new{id=v.Id},ToDto(v));
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult<VoucherDto>> Update(int id, [FromBody] VoucherUpdateDto dto)
        {
            if(dto.ToNumber<dto.FromNumber) return BadRequest(new{message="ToNumber must be >= FromNumber."});
            var v=await _db.Vouchers.Include(x=>x.Rep).FirstOrDefaultAsync(x=>x.Id==id); if(v==null) return NotFound(new{message=$"Voucher {id} not found."});
            v.RepId=dto.RepId; v.FromNumber=dto.FromNumber; v.ToNumber=dto.ToNumber; v.CountVouchers=dto.ToNumber-dto.FromNumber+1; v.RecordBy=dto.RecordBy;
            await _db.SaveChangesAsync(); await _db.Entry(v).Reference(x=>x.Rep).LoadAsync(); return Ok(ToDto(v));
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        { var v=await _db.Vouchers.FindAsync(id); if(v==null) return NotFound(new{message=$"Voucher {id} not found."}); _db.Vouchers.Remove(v); await _db.SaveChangesAsync(); return Ok(new{message="Voucher deleted."}); }
        private static VoucherDto ToDto(Voucher v) => new(){Id=v.Id,RepId=v.RepId,RepName=v.Rep?.RepName,FromNumber=v.FromNumber,ToNumber=v.ToNumber,CountVouchers=v.CountVouchers,RecordBy=v.RecordBy,RecordTime=v.RecordTime};
    }
}
