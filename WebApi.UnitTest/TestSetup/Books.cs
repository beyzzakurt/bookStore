using PatikaProject.DbOperations;
using PatikaProject.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.UnitTest.TestSetup
{
    public static class Books
    {
        public static void AddBooks(this BookDbContext context)
        {
            context.Books.AddRange(
               new Book { Title = "Lean Startup", GenreId = 1, AuthorId = 1, PageCount = 200, PublishDate = new DateTime(2001, 06, 04)},
               new Book { Title = "Herland",GenreId = 2, PageCount = 350, AuthorId = 2, PublishDate = new DateTime(2010, 05, 23)},
               new Book {Title = "Dune", GenreId = 2, PageCount = 350, AuthorId = 2, PublishDate = new DateTime(2020, 02, 25)}
            );


        }
    }
}
