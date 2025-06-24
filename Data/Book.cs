using System.Data;

namespace BookManagement.Data
{
    public class Book
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int NoOfPages { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int LanguageId { get; set; }
        public int? AuthorId { get; set; }
        public Language? Language { get; set; }
        public Author? Author { get; set; }
    }
}