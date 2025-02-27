using System.ComponentModel.DataAnnotations.Schema;

namespace PatikaProject.Entity
{
    public class Genre
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; } = true;
        public List<Book> Books { get; set; } = new List<Book>();
    }
}
