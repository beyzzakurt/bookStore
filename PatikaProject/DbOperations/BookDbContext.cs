using Microsoft.EntityFrameworkCore;
using PatikaProject.Entity;

namespace PatikaProject.DbOperations
{
    public class BookDbContext : DbContext
    {
        public BookDbContext(DbContextOptions<BookDbContext> options) : base(options) { }

        public DbSet<Book> Books { get; set; }


    }
}
