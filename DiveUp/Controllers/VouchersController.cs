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
    public class VouchersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public VouchersController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>Get all vouchers - optional search by from, to, or rep name</summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VoucherDto>>> GetAll([FromQuery] string? search)
        {
            var query = _context.Vouchers.Include(v => v.Rep).AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                var s = search.Trim().ToLower();
                query = query.Where(v =>
                    v.VoucherFrom.ToLower().Contains(s) ||
                    v.VoucherTo.ToLower().Contains(s) ||
                    (v.Rep != null && v.Rep.RepName.ToLower().Contains(s))
                );
            }

            var list = await query
                .OrderByDescending(v => v.RecordTime)
                .Select(v => new VoucherDto
                {
                    Id = v.Id,
                    VoucherFrom = v.VoucherFrom,
                    VoucherTo = v.VoucherTo,
                    VoucherCount = v.VoucherCount,
                    RepId = v.RepId,
                    RepName = v.Rep != null ? v.Rep.RepName : null,
                    RecordBy = v.RecordBy,
                    RecordTime = v.RecordTime
                })
                .ToListAsync();

            return Ok(list);
        }

        /// <summary>Get voucher by ID</summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<VoucherDto>> GetById(int id)
        {
            var v = await _context.Vouchers.Include(x => x.Rep).FirstOrDefaultAsync(x => x.Id == id);
            if (v == null)
                return NotFound(new { message = $"Voucher with ID {id} not found." });

            return Ok(ToDto(v));
        }

        /// <summary>Create a new voucher</summary>
        [HttpPost]
        public async Task<ActionResult<VoucherDto>> Create([FromBody] VoucherCreateDto dto)
        {
            var voucher = new Voucher
            {
                VoucherFrom = dto.VoucherFrom,
                VoucherTo = dto.VoucherTo,
                VoucherCount = dto.VoucherCount,
                RepId = dto.RepId,
                RecordBy = dto.RecordBy,
                RecordTime = DateTime.Now
            };

            _context.Vouchers.Add(voucher);
            await _context.SaveChangesAsync();
            await _context.Entry(voucher).Reference(x => x.Rep).LoadAsync();

            return CreatedAtAction(nameof(GetById), new { id = voucher.Id }, ToDto(voucher));
        }

        /// <summary>Update a voucher</summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<VoucherDto>> Update(int id, [FromBody] VoucherUpdateDto dto)
        {
            var voucher = await _context.Vouchers.Include(x => x.Rep).FirstOrDefaultAsync(x => x.Id == id);
            if (voucher == null)
                return NotFound(new { message = $"Voucher with ID {id} not found." });

            voucher.VoucherFrom = dto.VoucherFrom;
            voucher.VoucherTo = dto.VoucherTo;
            voucher.VoucherCount = dto.VoucherCount;
            voucher.RepId = dto.RepId;
            voucher.RecordBy = dto.RecordBy;

            await _context.SaveChangesAsync();
            await _context.Entry(voucher).Reference(x => x.Rep).LoadAsync();

            return Ok(ToDto(voucher));
        }

        /// <summary>Delete a voucher</summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var voucher = await _context.Vouchers.FindAsync(id);
            if (voucher == null)
                return NotFound(new { message = $"Voucher with ID {id} not found." });

            _context.Vouchers.Remove(voucher);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Voucher deleted successfully." });
        }

        private static VoucherDto ToDto(Voucher v) => new()
        {
            Id = v.Id,
            VoucherFrom = v.VoucherFrom,
            VoucherTo = v.VoucherTo,
            VoucherCount = v.VoucherCount,
            RepId = v.RepId,
            RepName = v.Rep?.RepName,
            RecordBy = v.RecordBy,
            RecordTime = v.RecordTime
        };
    }
}
