using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using PatikaProject.Application.BookOperations.Commands.CreateBook;
using PatikaProject.DbOperations;
using PatikaProject.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.UnitTest.TestSetup;

namespace WebApi.UnitTest.Application.BookOperations.Commands.CreateCommand
{
    public class CreateBookTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookDbContext _context;
        private readonly IMapper _mapper;

        public CreateBookTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenAlreadyExistBookTitleIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            //arrange (hazırlık)

            var book = new Book() { 
                Title = "Les Miserables", 
                PageCount = 100, 
                PublishDate = new System.DateTime(1990, 01, 10),
                GenreId = 1
            };
            _context.Books.Add(book);
            _context.SaveChanges();

            CreateBookCommand command = new CreateBookCommand(_context, _mapper);
            command.Model = new CreateBookModel() { Title = book.Title};


            //act (çalıştırma) & assert (doğrulama) 

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap zaten mevcut");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Book_ShouldBeCreated()
        {
            //arrange
            CreateBookCommand command = new CreateBookCommand(_context, _mapper);
            CreateBookModel model = new CreateBookModel() { Title = "Hobbit", GenreId = 1, PageCount = 100, PublishDate = DateTime.Now.Date.AddYears(-5) };
            command.Model = model;

            //act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            //assert
            var book = _context.Books.SingleOrDefault(book => book.Title ==  model.Title);
            book.Should().NotBeNull();
            book.PageCount.Should().Be(model.PageCount);
            book.PublishDate.Should().Be(model.PublishDate);
            book.GenreId.Should().Be(model.GenreId);
        }
    }
}
