using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PatikaProject.DbOperations;
using PatikaProject.Entity;

namespace PatikaProject.BookOperations.GetBooks
{
    public class GetBooksQuery
    {
        private readonly IMapper _mapper;

        private readonly BookDbContext _dbContext;
        public GetBooksQuery(BookDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<BookViewModel> Handle()
        {
            var bookList = _dbContext.Books.OrderBy(x => x.Id).ToList<Book>();

            List<BookViewModel> viewModel = _mapper.Map<List<BookViewModel>>(bookList);
           
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
