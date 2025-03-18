using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PatikaProject.DbOperations;
using PatikaProject.Entity;

namespace PatikaProject.Application.BookOperations.Queries.GetBooks
{
    public class GetBooksQuery
    {
        private readonly IMapper _mapper;

        private readonly IBookDbContext _dbContext;
        public GetBooksQuery(IBookDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<BookViewModel> Handle()
        {
            var bookList = _dbContext.Books.Include(x => x.Genre).Include(x => x.Author).OrderBy(x => x.Id).ToList();

            List<BookViewModel> viewModel = _mapper.Map<List<BookViewModel>>(bookList);

            return viewModel;
        }
    }

    public class BookViewModel
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Author { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }

    }
}
