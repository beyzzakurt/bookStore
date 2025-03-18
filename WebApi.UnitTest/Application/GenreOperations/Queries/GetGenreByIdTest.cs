using AutoMapper;
using FluentAssertions;
using PatikaProject.Application.BookOperations.Queries.GetBookById;
using PatikaProject.Application.GenreOperations.Queries.GetGenreDetail;
using PatikaProject.DbOperations;
using PatikaProject.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.UnitTest.TestSetup;

namespace WebApi.UnitTest.Application.GenreOperations.Queries
{
    public class GetGenreByIdTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookDbContext _context;
        private readonly IMapper _mapper;

        public GetGenreByIdTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        private int GetNonExistingGenreId()
        {
            return _context.Genres.Any() ? _context.Genres.Max(x => x.Id) + 1 : 0;
        }

        [Fact]
        public void WhenGenreIdDoesNotExist_InvalidOperationException_ShouldBeThrown()
        {
            // Arrange
            var query = new GetGenreDetailQuery(_context, _mapper);
            query.GenreId = GetNonExistingGenreId();

            // Act & Assert
            FluentActions
                .Invoking(() => query.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("Kitap türü bulunamadı!");
        }

        [Fact]
        public void WhenGenreIdExists_Genre_ShouldBeReturned()
        {

            // Arrange
            var genre = new Genre() { Name = "Loyalty" };
            _context.Genres.Add(genre);
            _context.SaveChanges();


            GetGenreDetailQuery query = new GetGenreDetailQuery(_context, _mapper);
            query.GenreId = genre.Id;

            // Act
            var result = query.Handle();

            // Assert
            result.Should().NotBeNull();
            result.Name.Should().Be(genre.Name);
        }
    }
}
