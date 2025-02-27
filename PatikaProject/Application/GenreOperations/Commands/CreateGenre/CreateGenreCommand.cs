using PatikaProject.DbOperations;

namespace PatikaProject.Application.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommand
    {
        public CreateGenreModel Model;

        private readonly IBookDbContext _context;

        public CreateGenreCommand(IBookDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var genre = _context.Genres.SingleOrDefault(x => x.Name == Model.Name);

            if (genre is not null)
                throw new InvalidOperationException("Kitap türü zaten mevcut.");

            genre = new Entity.Genre();
            genre.Name = Model.Name;
            _context.Genres.Add(genre);
            _context.SaveChanges();

        }
    }

    public class CreateGenreModel
    {
        public string Name { get; set; }
    }
}
