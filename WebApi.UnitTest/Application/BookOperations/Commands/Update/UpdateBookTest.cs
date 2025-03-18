using FluentAssertions;
using PatikaProject.Application.BookOperations.Commands.UpdateBook;
using PatikaProject.DbOperations;
using PatikaProject.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.UnitTest.TestSetup;

namespace WebApi.UnitTest.Application.BookOperations.Commands.UpdateCommand
{
    public class UpdateBookTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookDbContext _context;

        public UpdateBookTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        private int GetNonExistingBookId()
        {
            return _context.Books.Any() ? _context.Books.Max(x => x.Id) + 1 : 0;
        }

        [Fact] 
        public void WhenBookIdDoesNotExist_InvalidOperationException_ShouldBeReturn()
        {
            //arrange
            UpdateBookCommand command = new UpdateBookCommand(_context);
            command.BookId = GetNonExistingBookId();
            command.Model = new UpdateBookModel() { Title = "Updated Title", GenreId = 1 };

            //act & assert
            FluentActions
                .Invoking( () => command.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("Güncellenecek kitap bulunamadı!");
        }


        [Fact]
        public void WhenValidInputsAreGiven_Book_ShouldBeUpdated()
        {
            // arrange
            var book = new Book()
            {
                Title = "Anna Karenina",
                PageCount = 500,
                PublishDate = new DateTime(1895, 10, 10),
                GenreId = 2
            };
            _context.Books.Add(book);
            _context.SaveChanges();

            UpdateBookCommand command = new UpdateBookCommand( _context);
            command.BookId = book.Id;
            command.Model = new UpdateBookModel() { Title = "Anna Karenina Nowadays", GenreId = 3 };

            //act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            //assert
            var updatedBook = _context.Books.SingleOrDefault(x => x.Id == book.Id);
            updatedBook.Should().NotBeNull();
            updatedBook.Title.Should().Be("Anna Karenina Nowadays");
            updatedBook.GenreId.Should().Be(3);
        }

    }
}
