using AutoMapper;
using PatikaProject.DbOperations;

namespace PatikaProject.Application.GenreOperations.Queries.GetGenres
{
    public class GetGenresQuery
    {
        public readonly IBookDbContext _context;

        public readonly IMapper _mapper;

        public GetGenresQuery(IBookDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<GenresViewModel> Handle()
        {
            var genres = _context.Genres.Where(x => x.IsActive).OrderBy(x => x.Id);

            List<GenresViewModel> obj = _mapper.Map<List<GenresViewModel>>(genres);
            return obj;
        }
 
    }

    public class GenresViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }
}
