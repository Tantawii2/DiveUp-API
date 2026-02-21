using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DiveUp.Data;
using DiveUp.DTOs;
using DiveUp.Models;

namespace DiveUp.Controllers.SystemOperation.Codes.Functions
{
    [ApiController][Route("api/[controller]")][Produces("application/json")]
    public class RatesController : ControllerBase
    {
        private readonly AppDbContext _db;
        public RatesController(AppDbContext db) => _db = db;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RateDto>>> GetAll([FromQuery] string? currency)
        {
            var q = _db.Rates.AsQueryable();
            if (!string.IsNullOrWhiteSpace(currency)) q=q.Where(r=>r.Currency.ToLower()==currency.Trim().ToLower());
            return Ok(await q.OrderByDescending(r=>r.FromDate).Select(r=>ToDto(r)).ToListAsync());
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<RateDto>> GetById(int id)
        { var r=await _db.Rates.FindAsync(id); return r==null?NotFound(new{message=$"Rate {id} not found."}):Ok(ToDto(r)); }
        [HttpPost]
        public async Task<ActionResult<RateDto>> Create([FromBody] RateCreateDto dto)
        {
            if(dto.ToDate < dto.FromDate) return BadRequest(new{message="ToDate must be >= FromDate."});
            var r=new Rate{FromDate=dto.FromDate,ToDate=dto.ToDate,Currency=dto.Currency,RateValue=dto.RateValue};
            _db.Rates.Add(r); await _db.SaveChangesAsync(); return CreatedAtAction(nameof(GetById),new{id=r.Id},ToDto(r));
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult<RateDto>> Update(int id, [FromBody] RateUpdateDto dto)
        {
            if(dto.ToDate < dto.FromDate) return BadRequest(new{message="ToDate must be >= FromDate."});
            var r=await _db.Rates.FindAsync(id); if(r==null) return NotFound(new{message=$"Rate {id} not found."});
            r.FromDate=dto.FromDate; r.ToDate=dto.ToDate; r.Currency=dto.Currency; r.RateValue=dto.RateValue;
            await _db.SaveChangesAsync(); return Ok(ToDto(r));
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        { var r=await _db.Rates.FindAsync(id); if(r==null) return NotFound(new{message=$"Rate {id} not found."}); _db.Rates.Remove(r); await _db.SaveChangesAsync(); return Ok(new{message="Rate deleted."}); }
        private static RateDto ToDto(Rate r) => new(){Id=r.Id,FromDate=r.FromDate,ToDate=r.ToDate,Currency=r.Currency,RateValue=r.RateValue};
    }
}
