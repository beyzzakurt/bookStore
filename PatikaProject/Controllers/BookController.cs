using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using PatikaProject.Application.BookOperations.Commands.CreateBook;
using PatikaProject.Application.BookOperations.Commands.DeleteBook;
using PatikaProject.Application.BookOperations.Commands.UpdateBook;
using PatikaProject.Application.BookOperations.Queries.GetBookById;
using PatikaProject.Application.BookOperations.Queries.GetBooks;
using PatikaProject.DbOperations;
using PatikaProject.Entity;

namespace PatikaProject.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IMapper _mapper;

        private readonly IBookDbContext _context;

        public BookController(IBookDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        [HttpGet]
        public IActionResult GetBook()
        {
            GetBooksQuery query = new GetBooksQuery(_context, _mapper);
            var result = query.Handle();
            return Ok(result);
        }


        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            GetByIdViewModel result;
            GetBookByIdQuery query = new GetBookByIdQuery(_context, _mapper);
           
            query.BookId = id;

            GetByIdQueryValidator validator = new GetByIdQueryValidator();
            validator.ValidateAndThrow(query);

            result = query.Handle();
            
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
            CreateBookCommand bookCommand = new CreateBookCommand(_context, _mapper);
            bookCommand.Model = newBook;

            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            validator.ValidateAndThrow(bookCommand);
            
            bookCommand.Handle();   
            return Ok();
        }


        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updatedBook)
        {
           
            UpdateBookCommand updateCommand = new UpdateBookCommand(_context);
            updateCommand.BookId = id;
            updateCommand.Model = updatedBook;

            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            validator.ValidateAndThrow(updateCommand);

            updateCommand.Handle();
                      
            return Ok();

        }


        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            
            DeleteBookCommand deleteCommand = new DeleteBookCommand(_context);
            deleteCommand.BookId = id;

            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
            validator.ValidateAndThrow(deleteCommand);

            deleteCommand.Handle();
            
            return Ok();
         }


    }
}
//readonly deðiþken uygulama içerisinde deðiþtirilemez sadece construcutor içinde set edilebilir.