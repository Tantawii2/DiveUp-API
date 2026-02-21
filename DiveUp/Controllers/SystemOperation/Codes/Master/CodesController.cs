using Microsoft.AspNetCore.Mvc;
using DiveUp.DTOs.SystemOperation.Codes.Master;

namespace DiveUp.Controllers.SystemOperation.Codes.Master
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class CodesController : ControllerBase
    {
        /// <summary>Returns the list of all code menu items (names only)</summary>
        [HttpGet]
        public ActionResult<IEnumerable<CodeMenuItemDto>> GetMenu()
        {
            var menu = new List<CodeMenuItemDto>
            {
                new() { Key = "agents",                  DisplayName = "Code Agent" },
                new() { Key = "boats",                   DisplayName = "Code Boat" },
                new() { Key = "excursions",              DisplayName = "Code Excursion" },
                new() { Key = "excursionsuppliers",      DisplayName = "Code Excursion Supplier" },
                new() { Key = "excursioncostsellings",   DisplayName = "Code Excursion Cost Selling" },
                new() { Key = "reps",                    DisplayName = "Code Rep" },
                new() { Key = "guides",                  DisplayName = "Code Guide" },
                new() { Key = "hotels",                  DisplayName = "Code Hotel" },
                new() { Key = "hoteldestinations",       DisplayName = "Code Hotel Destination" },
                new() { Key = "nationalities",           DisplayName = "Code Nationality" },
                new() { Key = "pricelists",              DisplayName = "Code Price List" },
                new() { Key = "rates",                   DisplayName = "Code Rate" },
                new() { Key = "transportationtypes",     DisplayName = "Code Transportation Type" },
                new() { Key = "transportationsuppliers", DisplayName = "Code Transportation Supplier" },
                new() { Key = "transportationcosts",     DisplayName = "Code Transportation Cost" },
                new() { Key = "repvouchers",             DisplayName = "Code Rep Voucher" },
            };
            return Ok(menu);
        }
    }
}
