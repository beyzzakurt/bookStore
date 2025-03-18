using AutoMapper;
using FluentAssertions;
using PatikaProject.Application.GenreOperations.Commands.DeleteGenre;
using PatikaProject.DbOperations;
using PatikaProject.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.UnitTest.TestSetup;

namespace WebApi.UnitTest.Application.GenreOperations.Commands.Delete
{
    public class DeleteGenreTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookDbContext _context;
        private readonly IMapper _mapper;

        public DeleteGenreTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenGenreDoesNotExist_InvalidOperationException_ShouldBeThrown()
        {
            // Arrange 
            var command = new DeleteGenreCommand(_context);
            var maxId = _context.Books.Any() ? _context.Books.Max(b => b.Id) : 0;
            command.GenreId = maxId + 1;

            // Act & Assert 
            FluentActions
                .Invoking(() => command.Handle()) 
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("Kitap türü bulunamadı!");
        }


        [Fact]
        public void WhenGenreExists_Genre_ShouldBeDeleted()
        {
            // Arrange 
            var genre = new Genre() { Name = "Fantasy" };
            _context.Genres.Add(genre);
            _context.SaveChanges();

            var command = new DeleteGenreCommand(_context);
            command.GenreId = genre.Id;

            // Act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            // Assert 
            var deletedGenre = _context.Genres.FirstOrDefault(g => g.Id == genre.Id);
            deletedGenre.Should().BeNull();
        }

    }
}
