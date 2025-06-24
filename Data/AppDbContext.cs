using Microsoft.EntityFrameworkCore;

namespace BookManagement.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Currency>().HasData(
                new Currency { Id = 1, Title = "Rupee", Description = "Indian Rupee" },
                new Currency { Id = 2, Title = "EUR", Description = "Euro" },
                new Currency { Id = 3, Title = "GBP", Description = "British Pound Sterling" },
                new Currency { Id = 4, Title = "USD", Description = "United States Dollar" }
            );

            modelBuilder.Entity<Language>().HasData(
                new Language { Id = 1, Title = "English", Description = "English Language" },
                new Language { Id = 2, Title = "Hindi", Description = "Hindi Language" },
                new Language { Id = 3, Title = "Spanish", Description = "Spanish Language" },
                new Language { Id = 4, Title = "French", Description = "French Language" }
            );
        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<BookPrice> BookPrices { get; set; }
        public DbSet<Author> Authors { get; set; }
    }
}