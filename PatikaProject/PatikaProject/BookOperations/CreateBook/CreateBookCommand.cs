using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PatikaProject.DbOperations;

namespace PatikaProject.BookOperations.CreateBook
{
    public class CreateBookCommand
    {
        public CreateBookModel model { get; set; }

        private readonly BookDbContext _dbContext;

        public CreateBookCommand(BookDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            
            var book = _dbContext.Books.SingleOrDefault(x => x.Title == model.Title);

            if (book != null)
                throw new InvalidOperationException("Kitap zaten mevcut");
            book = new Entity.Book();
            book.Title = model.Title;
            book.GenreId = model.GenreId;
            book.PageCount = model.PageCount;
            book.PublishDate = model.PublishDate;

            _dbContext.Books.Add(book);
            _dbContext.SaveChanges();
           
        }
    }


    public class CreateBookModel
    {
        public string Title { get; set; }
        public int GenreId { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
    }
}
