using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PatikaProject.DbOperations;

namespace PatikaProject.Application.AuthorOperations.Queries.GetAuthorById
{
    public class GetAuthorById
    {
        private readonly IBookDbContext _context;

        private readonly IMapper _mapper;

        public int AuthorId { get; set; }

        public GetAuthorById(IBookDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public AuthorByIdViewModel Handle()
        {
            var author = _context.Authors.Include(a => a.Books).SingleOrDefault(x => x.Id == AuthorId);
           
            if (author is null)
                throw new InvalidOperationException("Aranan yazar bulunamadı.");

            AuthorByIdViewModel viewModel = _mapper.Map<AuthorByIdViewModel>(author);

            return viewModel;
        }
    }

    public class AuthorByIdViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Birthday { get; set; }
        public List<string> Books { get; set; }
    }
}
