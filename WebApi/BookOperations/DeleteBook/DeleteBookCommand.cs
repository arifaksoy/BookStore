using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApi.BookOperations.GetBooks;
using WebApi.DBOperations;

namespace WebApi.BookOperations.DeleteBook
{
    public class DeleteBookCommand
    {
        private readonly BookStoreDbContext _context;

        public int BookId { get; set; }
        public DeleteBookCommand(BookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var book = _context.Books.Where(b=>b.Id == BookId).SingleOrDefault();

            if(book is null)
            {
                throw new InvalidOperationException("Bu kitap bulunamadı!");
            } 

            _context.Books.Remove(book);
            _context.SaveChanges();
        }
    }
}