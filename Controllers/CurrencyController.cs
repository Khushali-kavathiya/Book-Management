using BookManagement.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        public CurrencyController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet("GetAllCurrencies")]
        public async Task<IActionResult> GetAllCurrencies()
        {
            //var result = _appDbContext.Currencies.ToList();
            // var result = (from currencies in _appDbContext.Currencies
            //               select currencies).ToList();     // If you not write ToList() some time error will occur

            var result = await _appDbContext.Currencies.ToListAsync();
            return Ok(result);
        }

        [HttpGet("GetCurrencyById/{id}")]

        public async Task<IActionResult> GetCurrencyById([FromRoute] int id)
        {
            var currency = await _appDbContext.Currencies.FindAsync(id);
            if (currency == null)
            {
                return NotFound();
            }
            return Ok(currency);
        }

        [HttpGet("GetCurrencyByTitle/{title}")]
        public async Task<IActionResult> GetCurrencyByTitle([FromRoute] string title)
        {
            //var result = await _appDbContext.Currencies.Where(x => x.Title == title).FirstOrDefaultAsync();  // it check the all data of database and return the first one that matches the condition
            var result = await _appDbContext.Currencies.FirstOrDefaultAsync(x => x.Title == title);   // if we check condition with FirstOrDefultAsync give more performance than Where
            return Ok(result);
        }

        // [HttpGet("GetCurrencyByTitleOrDescriptions/{title}")]
        // public async Task<IActionResult> GetCurrencyByTitleOrDescriptions([FromRoute] string title, [FromQuery] string? Description)
        // {
        //     var result = await _appDbContext.Currencies
        //                         .FirstOrDefaultAsync(x =>
        //                         x.Title == title
        //                         && (string.IsNullOrEmpty(Description) || x.Description == Description)
        //                          );   
        //     return Ok(result);
        // }

        [HttpGet("GetCurrencies/{name}")]

        public async Task<IActionResult> GetRecord([FromRoute] string name, [FromQuery] string? description)
        {
            var result = await _appDbContext.Currencies
                .Where(x => x.Title == name
                && (string.IsNullOrEmpty(description) || x.Description == description)
                ).ToListAsync();
            return Ok(result);
        }

        // [HttpPost("all")]
        // public async Task<IActionResult> GetIdsData([FromBody] List<int> ids)
        // {
        //     // var ids = new List<int> { 1, 2, 5 };
        //     var result = await _appDbContext.Currencies
        //         .Where(x => ids.Contains(x.Id))
        //         .ToListAsync();
        //     return Ok(result);
        // }

        // [HttpPost("all")]
        // public async Task<IActionResult> GetIdsData([FromBody] List<int> ids)
        // {
        //     var result = await _appDbContext.Currencies
        //         .Where(x => ids.Contains(x.Id))
        //         .Select(x => new Currency   // using currency model to get perticular column data
        //         {
        //             Id = x.Id,
        //             Title = x.Title,
        //         }
        //         )   // Get perticular column data from the database
        //         .ToListAsync();
        //     return Ok(result);
        // }
        
        [HttpPost("all")]
        public async Task<IActionResult> GetIdsData([FromBody] List<int> ids)
        {
            var result = await _appDbContext.Currencies
                .Where(x => ids.Contains(x.Id))
                .Select(x => new    // use anonymous type to get specific columns
                {
                    CurrencyId = x.Id,
                    CurrencyTitle = x.Title,
                }
                )   // Get perticular column data from the database
                .ToListAsync();
            return Ok(result);
        }

    }
}