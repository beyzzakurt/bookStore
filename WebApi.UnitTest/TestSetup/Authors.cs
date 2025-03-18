using PatikaProject.DbOperations;
using PatikaProject.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.UnitTest.TestSetup
{
    public static class Authors
    {
        public static void AddAuthors(this BookDbContext context)
        {
            context.Authors.AddRange(
                    new Author
                    {
                        Name = "Beyza",
                        Surname = "Kurt",
                        BirthDate = new DateTime(2001, 01, 30)
                    },
                    new Author
                    {
                        Name = "Engin",
                        Surname = "Demiroğ",
                        BirthDate = new DateTime(1985, 02, 24)
                    });
        }
    }
}
