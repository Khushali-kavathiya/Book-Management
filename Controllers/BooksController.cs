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

        [HttpGet("GetBooks")]

        public async Task<IActionResult> GetBook()
        {
            var books = await _appDbContext.Books.Select(x => new
            {
                id = x.Id,
                BookTitle = x.Title,
                Author = x.Author.name,
                LanguageName = x.Language.Title
            }
            ).ToListAsync();

            return Ok(books);
        }

        [HttpGet("Books")]

        public async Task<IActionResult> GetBooks()
        {
            var books = await _appDbContext.Books
                                .Include(x => x.Author)
                                //.Include(x => x.Language)
                               // .ThenInclude(x => x.AuthorCity)
                                .ToListAsync();
            return Ok(books);                    
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

        [HttpDelete("DeleteBook/{BookId}")]
        public async Task<IActionResult> DeleteBook([FromRoute] int BookId)
        {
            // var book = new Book { Id = BookId };
            // _appDbContext.Entry(book).State = EntityState.Deleted;
            // await _appDbContext.SaveChangesAsync();

            // return Ok("Book deleted successfully");

            try
            {
                var book = new Book { Id = BookId };
                _appDbContext.Books.Remove(book);
                await _appDbContext.SaveChangesAsync();
                return Ok("Book deleted successfully");

            }
            catch (Exception)
            {
                return NotFound("Book not found");
            }

        }

        [HttpDelete("Bulk")]
        public async Task<IActionResult> BooksDelete()
        {
            var books = await _appDbContext.Books.Where(x => x.Id < 4).ExecuteDeleteAsync();
            return Ok($"{books} Books Deleted successfully");
        }

    }
}

 