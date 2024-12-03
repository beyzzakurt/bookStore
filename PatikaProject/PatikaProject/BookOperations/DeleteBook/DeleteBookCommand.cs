using Microsoft.EntityFrameworkCore;
using PatikaProject.DbOperations;

namespace PatikaProject.BookOperations.DeleteBook
{
    public class DeleteBookCommand
    {
        public int BookId { get; set; }
        private readonly BookDbContext _dbContext;
        public DeleteBookCommand(BookDbContext dbContext)
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
