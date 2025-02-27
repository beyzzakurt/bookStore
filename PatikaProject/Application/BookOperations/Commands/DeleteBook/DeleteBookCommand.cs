using Microsoft.EntityFrameworkCore;
using PatikaProject.DbOperations;

namespace PatikaProject.Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommand
    {
        public int BookId { get; set; }

        private readonly IBookDbContext _dbContext;

        public DeleteBookCommand(IBookDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(x => x.Id == BookId);
            if (book == null)
                throw new InvalidOperationException("Silinecek kitap bulunamadı!");

            _dbContext.Books.Remove(book);
            _dbContext.SaveChanges();

        }
    }
}
