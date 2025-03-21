﻿using Microsoft.EntityFrameworkCore;
using PatikaProject.DbOperations;

namespace PatikaProject.Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommand
    {
        public int BookId { get; set; }
        public UpdateBookModel Model { get; set; }

        private readonly IBookDbContext _dbContext;
        public UpdateBookCommand(IBookDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(x => x.Id == BookId);

            if (book == null)
                throw new InvalidOperationException("Güncellenecek kitap bulunamadı!");

            book.GenreId = Model.GenreId != default ? Model.GenreId : book.GenreId;
            book.Title = Model.Title != default ? Model.Title : book.Title;

            _dbContext.SaveChanges();

        }
    }

    public class UpdateBookModel
    {
        public string Title { get; set; }
        public int GenreId { get; set; }

    }
}
