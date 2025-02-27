using AutoMapper;
using PatikaProject.DbOperations;
using PatikaProject.Entity;

namespace PatikaProject.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthor
    {
        public int AuthorId { get; set; }

        public UpdateAuthorViewModel Model { get; set; }

        private readonly IBookDbContext _context;

        private readonly IMapper _mapper;

        public UpdateAuthor(IBookDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            var author = _context.Authors.SingleOrDefault(x => x.Id == AuthorId);

            if (author is null)
                throw new InvalidOperationException("Aranan yazar bulunamadı!");

           
            _mapper.Map(Model, author);
            _context.SaveChanges();
        }
    }

    public class UpdateAuthorViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
