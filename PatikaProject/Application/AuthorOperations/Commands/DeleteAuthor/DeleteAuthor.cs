using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PatikaProject.DbOperations;

namespace PatikaProject.Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthor
    {
        public int AuthorId { get; set; }

        private readonly IBookDbContext _context;

        public DeleteAuthor(IBookDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var author = _context.Authors.Include(x => x.Books).SingleOrDefault(x => x.Id == AuthorId);

            if (author == null)
                throw new InvalidOperationException("Silinecek yazar bulunamadı.");

            if (author.Books.Any())
                throw new InvalidOperationException("Yazarın yayında olan kitabı olduğu için yazar silinemez!");

            _context.Authors.Remove(author);
            _context.SaveChanges();
        }
    }

}
