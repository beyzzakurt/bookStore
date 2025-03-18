using AutoMapper;
using FluentAssertions;
using PatikaProject.Application.GenreOperations.Commands.CreateGenre;
using PatikaProject.DbOperations;
using PatikaProject.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.UnitTest.TestSetup;

namespace WebApi.UnitTest.Application.GenreOperations.Commands.Create
{
    public class CreateGenreTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookDbContext _context;
        private readonly IMapper _mapper;

        public CreateGenreTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenGenreNameIsGiven_InvalidOperationException_ShouldBeThrown()
        {
            //Arrange
            var genre = new Genre() { Name = "History" };
            _context.Genres.Add(genre);
            _context.SaveChanges();

            var command = new CreateGenreCommand(_context);
            command.Model = new CreateGenreModel() { Name = genre.Name };

            // Act & Assert 
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("Kitap türü zaten mevcut.");
        }

        [Fact]
        public void WhenNewGenreNameIsGiven_Genre_ShouldBeCreatedSuccessfully()
        {
            // Arrange
            var command = new CreateGenreCommand(_context);
            command.Model = new CreateGenreModel() { Name = "Non-Fiction" };

            // Act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            // Assert
            var genre = _context.Genres.FirstOrDefault(g => g.Name == "Non-Fiction");
            genre.Should().NotBeNull();
            genre.Name.Should().Be("Non-Fiction");

        }
    }
}
