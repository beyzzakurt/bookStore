using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using PatikaProject.Application.BookOperations.Commands.DeleteBook;
using PatikaProject.DbOperations;
using PatikaProject.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.UnitTest.TestSetup;

namespace WebApi.UnitTest.Application.BookOperations.Commands.DeleteCommand
{
    public class DeleteBookTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookDbContext _context;
        private readonly IMapper _mapper;

        public DeleteBookTest(CommonTestFixture testFixture) 
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenTheBookIdDoesNotExist_InvalidOperationException_ShouldBeThrown()
        {
            // Arrange 
            DeleteBookCommand command = new DeleteBookCommand(_context);
            var maxId = _context.Books.Any() ? _context.Books.Max(b => b.Id) : 0;
            command.BookId = maxId + 1;

            // Act & Assert
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Silinecek kitap bulunamadı!");
        }
 
        [Fact]
        public void WhenValidBookIdIsGiven_Book_ShouldBeDeleted()
        {
            // Arrange
            var book = new Book()
            {
                Title = "Give Or Take",
                PageCount = 200,
                PublishDate = new DateTime(2001, 05, 15),
                GenreId = 2
            };
            _context.Books.Add(book);
            _context.SaveChanges();

            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.BookId = book.Id;

            // Act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            // Assert
            var deletedBook = _context.Books.SingleOrDefault(x => x.Id == book.Id);
            deletedBook.Should().BeNull("Kitap başarıyla silinmiş olmalı.");
        }

    }
}
