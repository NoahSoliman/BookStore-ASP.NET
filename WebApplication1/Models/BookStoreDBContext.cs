

using Microsoft.EntityFrameworkCore;

namespace BookStore.Models
{
    public class BookStoreDBContext : DbContext
    {

        public BookStoreDBContext(DbContextOptions<BookStoreDBContext> options) : base(options) { }
    }

    public DbSet<Author> Authors { get; set; };

    public DbSet<Book> books { get; set; };
}
