using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using PatikaProject.Application.AuthorOperations.Commands.CreateAuthor;
using PatikaProject.Application.AuthorOperations.Commands.DeleteAuthor;
using PatikaProject.Application.AuthorOperations.Commands.UpdateAuthor;
using PatikaProject.Application.AuthorOperations.Queries.GetAuthorById;
using PatikaProject.Application.AuthorOperations.Queries.GetAuthors;
using PatikaProject.DbOperations;


namespace PatikaProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IBookDbContext _context;

        private IMapper _mapper;

        public AuthorController(IBookDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        [HttpGet]
        public IActionResult Get()
        {
            GetAuthors query = new GetAuthors(_context, _mapper);
            var result = query.Handle();

            return Ok(result);
        }


        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            GetAuthorById query = new GetAuthorById(_context, _mapper);
            query.AuthorId = id;

            GetAuthorByIdValidator validator = new GetAuthorByIdValidator();
            validator.ValidateAndThrow(query);

            var result = query.Handle();
            return Ok(result);
        }

  
        [HttpPost]
        public IActionResult Post([FromBody] CreateAuthorViewModel newAuthor)
        {
            CreateAuthor command = new CreateAuthor(_context, _mapper);
            command.Model = newAuthor;

            CreateAuthorValidator validator = new CreateAuthorValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok();
        }

  
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateAuthorViewModel updatedAuthor)
        {
            UpdateAuthor command = new UpdateAuthor(_context, _mapper);
            command.Model = updatedAuthor;
            command.AuthorId = id;

            UpdateAuthorValidator validator = new UpdateAuthorValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok();
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            DeleteAuthor command = new DeleteAuthor(_context);
            command.AuthorId = id;

            DeleteAuthorValidator validator = new DeleteAuthorValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok();

        }
    }
}
