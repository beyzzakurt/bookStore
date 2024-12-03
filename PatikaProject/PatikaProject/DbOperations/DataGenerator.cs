using Microsoft.EntityFrameworkCore;
using PatikaProject.Entity;

namespace PatikaProject.DbOperations
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider) 
        {
            using (var context = new BookDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookDbContext>>()))
            {
                if(context.Books.Any())
                {
                    return;
                }
                context.Books.AddRange(
                new Book
                {
                    
                    Title = "Lean Startup",
                    GenreId = 1, // Personal Growth
                    PageCount = 200,
                    PublishDate = new DateTime(2001, 06, 04)
                },

                new Book
                {
                    
                    Title = "Herland",
                    GenreId = 2, // Science Fiction
                    PageCount = 350,
                    PublishDate = new DateTime(2010, 05, 23)
                },

                new Book
                {
                    
                    Title = "Dune",
                    GenreId = 2, // Science Fiction
                    PageCount = 350,
                    PublishDate = new DateTime(2020, 02, 25)
                });
               context.SaveChanges();
            }
        }
    }
}
