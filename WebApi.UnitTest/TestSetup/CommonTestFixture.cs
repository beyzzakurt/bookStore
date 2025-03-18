using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PatikaProject;
using PatikaProject.DbOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.UnitTest.TestSetup
{
    public class CommonTestFixture
    {
        public BookDbContext Context { get; set; }
        public IMapper Mapper { get; set; }

        public CommonTestFixture()
        {
            var options = new DbContextOptionsBuilder<BookDbContext>().UseInMemoryDatabase(databaseName: "BookStoreTestDb").Options;

            Context = new BookDbContext(options);
            Context.Database.EnsureCreated();

            Context.AddBooks();
            Context.AddGenres();
            Context.AddAuthors();
            Context.SaveChanges();

            Mapper = new MapperConfiguration(cfg => { cfg.AddProfile<MappingProfile>(); }).CreateMapper();
        }
    }
}
