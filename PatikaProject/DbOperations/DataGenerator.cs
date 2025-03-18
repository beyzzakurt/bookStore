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

                context.Genres.AddRange(
                new Genre
                {
                    Name = "Personal Growth"
                },

                new Genre
                {
                    Name = "Science Fiction"
                },

                new Genre
                {
                    Name = "Romance"
                }
                 );


                context.Authors.AddRange(
                    new Author
                    {
                        Name = "Beyza",
                        Surname = "Kurt",
                        BirthDate = new DateTime(2001, 01, 30)
                    },
                    new Author
                    {
                        Name = "Engin",
                        Surname = "Demiroğ",
                        BirthDate = new DateTime(1985, 02, 24)
                    });

               context.SaveChanges(); // ID'leri oluşması için önce yazarlar kaydedilir 

               
               context.Books.AddRange(
               new Book
                {

                    Title = "Lean Startup",
                    GenreId = 1, // Personal Growth
                    AuthorId = 1,
                    PageCount = 200,
                    PublishDate = new DateTime(2001, 06, 04)
                },

               new Book
                {

                    Title = "Herland",
                    GenreId = 2, // Science Fiction
                    PageCount = 350,
                    AuthorId= 2,
                    PublishDate = new DateTime(2010, 05, 23)
                },

               new Book
                {

                    Title = "Dune",
                    GenreId = 3, // Romance
                    PageCount = 350,
                    AuthorId = 2,
                    PublishDate = new DateTime(2020, 02, 25)
                });

                context.SaveChanges();
            }
        }
    }
}
