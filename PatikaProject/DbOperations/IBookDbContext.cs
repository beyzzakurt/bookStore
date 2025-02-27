using Microsoft.EntityFrameworkCore;
using PatikaProject.Entity;

namespace PatikaProject.DbOperations
{
    public interface IBookDbContext
    {
        DbSet<Book> Books { get; set; }
        DbSet<Genre> Genres { get; set; }
        DbSet<Author> Authors { get; set; }
        int SaveChanges();
    }
}
