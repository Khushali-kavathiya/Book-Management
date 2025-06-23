using BookManagement.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class LanguageController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        public LanguageController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet("GetAllLanguages")]

        public async Task<IActionResult> GetLanguages()
        {
            var result = await _appDbContext.Languages.ToListAsync();
            return Ok(result);
        }
    }
}