using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DiveUp.Data;
using DiveUp.DTOs.SystemOperation.Codes.Functions;
using DiveUp.Models.SystemOperation.Codes.Functions;

namespace DiveUp.Controllers.SystemOperation.Codes.Functions
{
    [ApiController][Route("api/[controller]")][Produces("application/json")]
    public class ExcursionCostSellingsController : ControllerBase
    {
        private readonly AppDbContext _db;
        public ExcursionCostSellingsController(AppDbContext db) => _db = db;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExcursionCostSellingDto>>> GetAll(
            [FromQuery] int? excursionId, [FromQuery] int? agentId,
            [FromQuery] int? destinationId, [FromQuery] int? priceListId, [FromQuery] int? supplierId)
        {
            var q = _db.ExcursionCostSellings
                .Include(e=>e.PriceList).Include(e=>e.Excursion).Include(e=>e.Destination)
                .Include(e=>e.Agent).Include(e=>e.Supplier).AsQueryable();
            if(excursionId.HasValue)   q=q.Where(e=>e.ExcursionId==excursionId);
            if(agentId.HasValue)       q=q.Where(e=>e.AgentId==agentId);
            if(destinationId.HasValue) q=q.Where(e=>e.DestinationId==destinationId);
            if(priceListId.HasValue)   q=q.Where(e=>e.PriceListId==priceListId);
            if(supplierId.HasValue)    q=q.Where(e=>e.SupplierId==supplierId);
            return Ok(await q.OrderByDescending(e=>e.RecordTime).Select(e=>ToDto(e)).ToListAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ExcursionCostSellingDto>> GetById(int id)
        {
            var e=await _db.ExcursionCostSellings.Include(x=>x.PriceList).Include(x=>x.Excursion).Include(x=>x.Destination).Include(x=>x.Agent).Include(x=>x.Supplier).FirstOrDefaultAsync(x=>x.Id==id);
            return e==null?NotFound(new{message=$"ExcursionCostSelling {id} not found."}):Ok(ToDto(e));
        }

        [HttpPost]
        public async Task<ActionResult<ExcursionCostSellingDto>> Create([FromBody] ExcursionCostSellingCreateDto dto)
        {
            var e=new ExcursionCostSelling{
                PriceListId=dto.PriceListId,ExcursionId=dto.ExcursionId,DestinationId=dto.DestinationId,AgentId=dto.AgentId,SupplierId=dto.SupplierId,
                SellingAdlEGP=dto.SellingAdlEGP,SellingAdlUSD=dto.SellingAdlUSD,SellingAdlEUR=dto.SellingAdlEUR,SellingAdlGBP=dto.SellingAdlGBP,
                SellingChdEGP=dto.SellingChdEGP,SellingChdUSD=dto.SellingChdUSD,SellingChdEUR=dto.SellingChdEUR,SellingChdGBP=dto.SellingChdGBP,
                CostAdlEGP=dto.CostAdlEGP,CostAdlUSD=dto.CostAdlUSD,CostAdlEUR=dto.CostAdlEUR,CostAdlGBP=dto.CostAdlGBP,
                CostChdEGP=dto.CostChdEGP,CostChdUSD=dto.CostChdUSD,CostChdEUR=dto.CostChdEUR,CostChdGBP=dto.CostChdGBP,
                NationalFeeAdlEGP=dto.NationalFeeAdlEGP,NationalFeeAdlUSD=dto.NationalFeeAdlUSD,
                NationalFeeChdEGP=dto.NationalFeeChdEGP,NationalFeeChdUSD=dto.NationalFeeChdUSD,
                RecordBy=dto.RecordBy,RecordTime=DateTime.UtcNow
            };
            _db.ExcursionCostSellings.Add(e); await _db.SaveChangesAsync();
            await _db.Entry(e).Reference(x=>x.PriceList).LoadAsync();
            await _db.Entry(e).Reference(x=>x.Excursion).LoadAsync();
            await _db.Entry(e).Reference(x=>x.Destination).LoadAsync();
            await _db.Entry(e).Reference(x=>x.Agent).LoadAsync();
            await _db.Entry(e).Reference(x=>x.Supplier).LoadAsync();
            return CreatedAtAction(nameof(GetById),new{id=e.Id},ToDto(e));
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ExcursionCostSellingDto>> Update(int id, [FromBody] ExcursionCostSellingUpdateDto dto)
        {
            var e=await _db.ExcursionCostSellings.Include(x=>x.PriceList).Include(x=>x.Excursion).Include(x=>x.Destination).Include(x=>x.Agent).Include(x=>x.Supplier).FirstOrDefaultAsync(x=>x.Id==id);
            if(e==null) return NotFound(new{message=$"ExcursionCostSelling {id} not found."});
            e.PriceListId=dto.PriceListId; e.ExcursionId=dto.ExcursionId; e.DestinationId=dto.DestinationId; e.AgentId=dto.AgentId; e.SupplierId=dto.SupplierId;
            e.SellingAdlEGP=dto.SellingAdlEGP; e.SellingAdlUSD=dto.SellingAdlUSD; e.SellingAdlEUR=dto.SellingAdlEUR; e.SellingAdlGBP=dto.SellingAdlGBP;
            e.SellingChdEGP=dto.SellingChdEGP; e.SellingChdUSD=dto.SellingChdUSD; e.SellingChdEUR=dto.SellingChdEUR; e.SellingChdGBP=dto.SellingChdGBP;
            e.CostAdlEGP=dto.CostAdlEGP; e.CostAdlUSD=dto.CostAdlUSD; e.CostAdlEUR=dto.CostAdlEUR; e.CostAdlGBP=dto.CostAdlGBP;
            e.CostChdEGP=dto.CostChdEGP; e.CostChdUSD=dto.CostChdUSD; e.CostChdEUR=dto.CostChdEUR; e.CostChdGBP=dto.CostChdGBP;
            e.NationalFeeAdlEGP=dto.NationalFeeAdlEGP; e.NationalFeeAdlUSD=dto.NationalFeeAdlUSD;
            e.NationalFeeChdEGP=dto.NationalFeeChdEGP; e.NationalFeeChdUSD=dto.NationalFeeChdUSD;
            e.RecordBy=dto.RecordBy;
            await _db.SaveChangesAsync();
            await _db.Entry(e).Reference(x=>x.PriceList).LoadAsync();
            await _db.Entry(e).Reference(x=>x.Excursion).LoadAsync();
            await _db.Entry(e).Reference(x=>x.Destination).LoadAsync();
            await _db.Entry(e).Reference(x=>x.Agent).LoadAsync();
            await _db.Entry(e).Reference(x=>x.Supplier).LoadAsync();
            return Ok(ToDto(e));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        { var e=await _db.ExcursionCostSellings.FindAsync(id); if(e==null) return NotFound(new{message=$"ExcursionCostSelling {id} not found."}); _db.ExcursionCostSellings.Remove(e); await _db.SaveChangesAsync(); return Ok(new{message="ExcursionCostSelling deleted."}); }

        private static ExcursionCostSellingDto ToDto(ExcursionCostSelling e) => new(){
            Id=e.Id, PriceListId=e.PriceListId, PriceListName=e.PriceList?.PriceListName,
            ExcursionId=e.ExcursionId, ExcursionName=e.Excursion?.ExcursionName,
            DestinationId=e.DestinationId, DestinationName=e.Destination?.DestinationName,
            AgentId=e.AgentId, AgentName=e.Agent?.AgentName, SupplierId=e.SupplierId, SupplierName=e.Supplier?.SupplierName,
            SellingAdlEGP=e.SellingAdlEGP, SellingAdlUSD=e.SellingAdlUSD, SellingAdlEUR=e.SellingAdlEUR, SellingAdlGBP=e.SellingAdlGBP,
            SellingChdEGP=e.SellingChdEGP, SellingChdUSD=e.SellingChdUSD, SellingChdEUR=e.SellingChdEUR, SellingChdGBP=e.SellingChdGBP,
            CostAdlEGP=e.CostAdlEGP, CostAdlUSD=e.CostAdlUSD, CostAdlEUR=e.CostAdlEUR, CostAdlGBP=e.CostAdlGBP,
            CostChdEGP=e.CostChdEGP, CostChdUSD=e.CostChdUSD, CostChdEUR=e.CostChdEUR, CostChdGBP=e.CostChdGBP,
            NationalFeeAdlEGP=e.NationalFeeAdlEGP, NationalFeeAdlUSD=e.NationalFeeAdlUSD,
            NationalFeeChdEGP=e.NationalFeeChdEGP, NationalFeeChdUSD=e.NationalFeeChdUSD,
            RecordBy=e.RecordBy, RecordTime=e.RecordTime
        };
    }
}
