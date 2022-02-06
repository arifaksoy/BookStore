using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.BookOperations.GetBookDetail
{
    public class GetBookDetailQuery
    {

        private readonly BookStoreDbContext _context;

        public int BookId { get; set; }
        public GetBookDetailQuery(BookStoreDbContext context)
        {
            _context = context;
        }

        public BookDetailViewModel Handle()
        {
            var book = _context.Books.Where(b=>b.Id == BookId).SingleOrDefault();
            if(book is null)
                throw new InvalidOperationException("Aradığınız kitap şuan mevcut değildir.");
            var vm = new BookDetailViewModel();
            vm.Title = book.Title;
            vm.Genre = ((GenreEnum) book.GenreId).ToString();
            vm.PageCount = book.PageCount;
            vm.PublishDate = book.PublishDate.Date.ToString("dd/MM/yyyy");
            return vm;
        }
    }

    public class BookDetailViewModel 
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
    }
}