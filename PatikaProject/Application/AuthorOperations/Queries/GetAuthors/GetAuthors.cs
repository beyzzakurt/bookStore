using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PatikaProject.DbOperations;

namespace PatikaProject.Application.AuthorOperations.Queries.GetAuthors
{
    public class GetAuthors
    {
        private readonly IBookDbContext _context;

        private readonly IMapper _mapper;

        public GetAuthors(IBookDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<AuthorViewModel> Handle()
        {
            var authorList = _context.Authors.Include(a => a.Books).OrderBy(x => x.Id).ToList();
            
            List<AuthorViewModel> viewModel =_mapper.Map<List<AuthorViewModel>>(authorList);

            return viewModel;
        }
    }

    public class AuthorViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public List<string> Books { get; set; }

    }
}
