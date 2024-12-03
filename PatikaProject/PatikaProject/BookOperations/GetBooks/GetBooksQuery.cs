using Microsoft.EntityFrameworkCore;
using PatikaProject.DbOperations;
using PatikaProject.Entity;

namespace PatikaProject.BookOperations.GetBooks
{
    public class GetBooksQuery
    {
        private readonly BookDbContext _dbContext;
        public GetBooksQuery(BookDbContext dbContext) 
        { 
            _dbContext = dbContext;
        }

        public List<BookViewModel> Handle()
        {
            var bookList = _dbContext.Books.OrderBy(x => x.Id).ToList<Book>();

            List<BookViewModel> viewModel = new List<BookViewModel>();
            foreach (var book in bookList)
            {
                viewModel.Add(new BookViewModel()
                {
                    Title = book.Title,
                    Genre = ((GenreEnum)book.GenreId).ToString(),
                    PageCount = book.PageCount,
                    PublishDate = book.PublishDate.Date.ToString("dd/mm/yyyy")
                });
            }
            return viewModel;
        }
    }

    public class BookViewModel
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
       
    }
}
