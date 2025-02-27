using AutoMapper;
using PatikaProject.DbOperations;
using PatikaProject.Entity;
using System.Runtime.CompilerServices;

namespace PatikaProject.Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthor
    {
        public CreateAuthorViewModel Model { get; set; }

        private readonly IBookDbContext _context;

        private readonly IMapper _mapper;

        public CreateAuthor(IBookDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            var author = _context.Authors.SingleOrDefault(x => x.Name == Model.Name);

            if (author != null)
                throw new InvalidOperationException("Yazar mevcut.");
               
            author = _mapper.Map<Author>(Model);
            
            _context.Authors.Add(author);
            _context.SaveChanges();

        }
    }

    public class CreateAuthorViewModel()
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Birthday { get; set; }
    }
}
