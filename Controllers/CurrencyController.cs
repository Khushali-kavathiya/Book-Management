using BookManagement.Data;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult GetAllCurrencies()
        {
            //var result = _appDbContext.Currencies.ToList();
            var result = from currencies in _appDbContext.Currencies
                          select currencies;     
            return Ok(result);
        }
    }
}