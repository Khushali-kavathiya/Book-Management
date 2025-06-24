using BookManagement.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookManagement.Models;

namespace BookManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class BooksController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        public BooksController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpPost("AddBook")]

        public async Task<IActionResult> AddBook([FromBody] Book book)
        {
            _appDbContext.Books.Add(book);
            await _appDbContext.SaveChangesAsync();

            return Ok(book);
        }
    }
}