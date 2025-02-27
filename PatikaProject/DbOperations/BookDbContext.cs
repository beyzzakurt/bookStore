using Microsoft.EntityFrameworkCore;
using PatikaProject.Entity;

namespace PatikaProject.DbOperations
{
    public class BookDbContext : DbContext, IBookDbContext
    {
        public BookDbContext(DbContextOptions<BookDbContext> options) : base(options) { }

        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Author> Authors { get; set; }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .HasOne(x => x.Author)
                .WithMany(a => a.Books)
                .HasForeignKey(x => x.AuthorId)
                .OnDelete(DeleteBehavior.Restrict); //Eğer bir yazara ait kitap varsa, önce kitapların silinmesi gerekiyor, aksi takdirde yazarı silemezsin.

            modelBuilder.Entity<Book>()
                .HasOne(x => x.Genre)
                .WithMany(b => b.Books)
                .HasForeignKey(x => x.GenreId);

        }
    }
}
