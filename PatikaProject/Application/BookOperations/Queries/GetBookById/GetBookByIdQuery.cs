using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PatikaProject.DbOperations;
using PatikaProject.Entity;

namespace PatikaProject.Application.BookOperations.Queries.GetBookById
{
    public class GetBookByIdQuery
    {
        private readonly IMapper _mapper;
        public int BookId { get; set; }

        private readonly IBookDbContext _dbContext;
        public GetBookByIdQuery(IBookDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public GetByIdViewModel Handle()
        {
            var book = _dbContext.Books.Include(x => x.Genre).Where(book => book.Id == BookId).SingleOrDefault();

            if (book is null)
                throw new InvalidOperationException("Kitap bulunamadı!");

            GetByIdViewModel viewModel = _mapper.Map<GetByIdViewModel>(book);

            return viewModel;

        }
    }

    public class GetByIdViewModel
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Author { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }

    }
}
