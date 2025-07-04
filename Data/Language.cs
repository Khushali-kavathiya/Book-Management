namespace BookManagement.Data
{
    public class Language
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
       
        public virtual ICollection<Book>? Books { get; set; }
    }
}