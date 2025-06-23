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
    }
}