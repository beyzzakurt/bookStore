using Microsoft.EntityFrameworkCore;
using PatikaProject.DbOperations;
using PatikaProject.Entity;

namespace PatikaProject.BookOperations.GetBookById
{
    public class GetBookByIdQuery
    {
        public int BookId { get; set; }

        private readonly BookDbContext _dbContext;
        public GetBookByIdQuery(BookDbContext dbContext) 
        { 
            _dbContext = dbContext;
        }

        public GetByIdViewModel Handle()
        {
            var book = _dbContext.Books.Where(book => book.Id == BookId).SingleOrDefault();

            if(book is null)
                throw new InvalidOperationException("Kitap bulunamadı!");
            
            GetByIdViewModel viewModel = new GetByIdViewModel();

            viewModel.Title = book.Title;
            viewModel.Genre = ((GenreEnum)book.GenreId).ToString();
            viewModel.PageCount = book.PageCount;
            viewModel.PublishDate = book.PublishDate.Date.ToString("dd/mm/yyyy");

            return viewModel;
           
        }
    }

    public class GetByIdViewModel
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }

    }
}
