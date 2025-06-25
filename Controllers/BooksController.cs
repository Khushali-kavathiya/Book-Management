using BookManagement.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        [HttpPost("BulkInsertData")]

        public async Task<IActionResult> InsertBook([FromBody] List<Book> books)
        {
            _appDbContext.Books.AddRange(books);
            await _appDbContext.SaveChangesAsync();

            return Ok(books);
        }

        [HttpPut("UpdateBook/{BookId}")]

        public async Task<IActionResult> updateBook([FromRoute] int BookId, [FromBody] Book book)
        {
            var existingBook = await _appDbContext.Books.FirstOrDefaultAsync(x => x.Id == BookId);
            if (existingBook == null)
            {
                return NotFound("Book not found");
            }
            existingBook.Title = book.Title;
            existingBook.Description = book.Description;
            existingBook.NoOfPages = book.NoOfPages;

            await _appDbContext.SaveChangesAsync();

            return Ok(existingBook);
        }

        [HttpPut("update")]

        public async Task<IActionResult> update([FromBody] Book book)
        {
            // _appDbContext.Books.Update(book);
            _appDbContext.Entry(book).State = EntityState.Modified;
            await _appDbContext.SaveChangesAsync();

            return Ok(book);
        }

        [HttpPut("bulk")]

        public async Task<IActionResult> BulkUpdate()
        {
            var c = await _appDbContext.Books
                    .Where(x => x.NoOfPages > 300)
                    .ExecuteUpdateAsync(x => x
                        .SetProperty(b => b.Title, b => b.Title + "Added")
                        .SetProperty(b => b.Description, b => b.Description + "Book"));

            return Ok($"{c} records updated successfully");
        }
        
                
    
     }
}