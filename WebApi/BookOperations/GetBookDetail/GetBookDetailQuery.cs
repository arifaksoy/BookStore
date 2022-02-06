using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.BookOperations.GetBookDetail
{
    public class GetBookDetailQuery
    {

        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public int BookId { get; set; }
        public GetBookDetailQuery(BookStoreDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public BookDetailViewModel Handle()
        {
            var book = _context.Books.Where(b=>b.Id == BookId).SingleOrDefault();
            if(book is null)
                throw new InvalidOperationException("Aradığınız kitap şuan mevcut değildir.");
            var vm = _mapper.Map<BookDetailViewModel>(book);
           
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