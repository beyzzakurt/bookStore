using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PatikaProject.DbOperations;
using PatikaProject.Entity;

namespace PatikaProject.BookOperations.GetBookById
{
    public class GetBookByIdQuery
    {
        private readonly IMapper _mapper;
        public int BookId { get; set; }

        private readonly BookDbContext _dbContext;
        public GetBookByIdQuery(BookDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public GetByIdViewModel Handle()
        {
            var book = _dbContext.Books.Where(book => book.Id == BookId).SingleOrDefault();

            if(book is null)
                throw new InvalidOperationException("Kitap bulunamadı!");

            GetByIdViewModel viewModel = _mapper.Map<GetByIdViewModel>(book);

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
