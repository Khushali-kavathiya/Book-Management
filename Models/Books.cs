namespace BookManagement.Models
{
    public class Books
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int NoOfPages { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int LanguageId { get; set; }
    }
}