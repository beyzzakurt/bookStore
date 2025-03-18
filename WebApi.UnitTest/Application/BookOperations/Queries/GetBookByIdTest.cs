using AutoMapper;
using FluentAssertions;
using PatikaProject.Application.BookOperations.Queries.GetBookById;
using PatikaProject.DbOperations;
using PatikaProject.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.UnitTest.TestSetup;

namespace WebApi.UnitTest.Application.BookOperations.Queries
{
    public class GetBookByIdTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookDbContext _context;
        private readonly IMapper _mapper;

        public GetBookByIdTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        private int GetNonExistingBookId()
        {
            return _context.Books.Any() ? _context.Books.Max(x => x.Id) + 1 : 0;
        }

        [Fact]
        public void WhenBookIdDoesNotExist_InvalidOperationException_ShouldBeThrown()
        {
            // arrange
            var query = new GetBookByIdQuery(_context, _mapper);
            query.BookId = GetNonExistingBookId();

            // act & assert
            FluentActions
                .Invoking(() => query.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("Kitap bulunamadı!");
        }

        [Fact]
        public void WhenBookIdExists_Book_ShouldBeReturned()
        {

            // arrange
            var book = new Book()
            {
                Title = "War And Peace",
                GenreId = 1,
                AuthorId = 1,
                PageCount = 650,
                PublishDate = new DateTime(1850, 01, 01),
            };
            _context.Books.Add(book);
            _context.SaveChanges();

            GetBookByIdQuery query = new GetBookByIdQuery(_context, _mapper);
            query.BookId = book.Id;

            //act
            var result = query.Handle();

            //assert
            result.Should().NotBeNull();
            result.Title.Should().Be(book.Title);
            result.PageCount.Should().Be(book.PageCount);
            result.PublishDate.Should().Be(book.PublishDate);
        }

    }
}
