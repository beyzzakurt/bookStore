using Microsoft.AspNetCore.Mvc;
using PatikaProject.BookOperations.CreateBook;
using PatikaProject.BookOperations.DeleteBook;
using PatikaProject.BookOperations.GetBookById;
using PatikaProject.BookOperations.GetBooks;
using PatikaProject.BookOperations.UpdateBook;
using PatikaProject.DbOperations;
using PatikaProject.Entity;

namespace PatikaProject.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private readonly BookDbContext _context;

        public BookController(BookDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public IActionResult GetBook()
        {
            GetBooksQuery query = new GetBooksQuery(_context);
            var result = query.Handle();
            return Ok(result);
        }


        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            GetByIdViewModel result;
            GetBookByIdQuery query = new GetBookByIdQuery(_context);
            try
            {
                query.BookId = id;
                result = query.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(result);
        }


        //[HttpGet]
        //public Book Get([FromQuery] string id)
        //{
        //    var book = BookList.Where(book => book.Id == Convert.ToInt32(id)).SingleOrDefault();
        //    return book;
        //}



        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {
            CreateBookCommand bookCommand = new CreateBookCommand(_context);
            try
            {
                bookCommand.model = newBook;
                bookCommand.Handle();
            }
            catch (Exception ex) 
            { 
                return BadRequest(ex.Message);
            }
            return Ok();
        }


        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updatedBook)
        {
            try
            {
                UpdateBookCommand updateCommand = new UpdateBookCommand(_context);
                updateCommand.BookId = id;
                updateCommand.Model = updatedBook;
                updateCommand.Handle();
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
           
            return Ok();

        }


        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            try
            {
                DeleteBookCommand deleteCommand = new DeleteBookCommand(_context);
                deleteCommand.BookId = id;
                deleteCommand.Handle();
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
            
            return Ok();
         }


    }
}
//readonly deðiþken uygulama içerisinde deðiþtirilemez sadece construcutor içinde set edilebilir.