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
    }
}