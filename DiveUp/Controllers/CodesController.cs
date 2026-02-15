using Microsoft.AspNetCore.Mvc;
using DiveUp.DTOs;

namespace DiveUp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class CodesController : ControllerBase
    {
        /// <summary>
        /// Get all code category names (for the Codes menu in the UI).
        /// Optional search to filter by name.
        /// </summary>
        [HttpGet]
        public ActionResult<IEnumerable<CodeCategoryDto>> GetCategories([FromQuery] string? search)
        {
            var categories = new List<CodeCategoryDto>
            {
                new() { Key = "agents",                  DisplayName = "Code Agent"                   },
                new() { Key = "boats",                   DisplayName = "Code Boat"                    },
                new() { Key = "excursions",              DisplayName = "Code Excursion"               },
                new() { Key = "excursionsuppliers",      DisplayName = "Code Excursion Supplier"      },
                new() { Key = "excursioncostselling",    DisplayName = "Code Excursion Cost Selling"  },
                new() { Key = "reps",                    DisplayName = "Code Rep"                     },
                new() { Key = "guides",                  DisplayName = "Code Guide"                   },
                new() { Key = "hotels",                  DisplayName = "Code Hotel"                   },
                new() { Key = "hoteldestinations",       DisplayName = "Code Hotel Destination"       },
                new() { Key = "nationalities",           DisplayName = "Code Nationality"             },
                new() { Key = "pricelists",              DisplayName = "Code Price List"              },
                new() { Key = "rates",                   DisplayName = "Code Rate"                    },
                new() { Key = "transportationtypes",     DisplayName = "Code Transportation Type"     },
                new() { Key = "transportationsuppliers", DisplayName = "Code Transportation Supplier" },
                new() { Key = "transportationcosts",     DisplayName = "Code Transportation Cost"     },
                new() { Key = "users",                   DisplayName = "Code User"                    },
                new() { Key = "userauthorities",         DisplayName = "Code User Authority"          },
                new() { Key = "vouchers",                DisplayName = "Code Rep Voucher"             },
            };

            if (!string.IsNullOrWhiteSpace(search))
            {
                var s = search.Trim().ToLower();
                categories = categories
                    .Where(c => c.DisplayName.ToLower().Contains(s))
                    .ToList();
            }

            return Ok(categories);
        }
    }
}
