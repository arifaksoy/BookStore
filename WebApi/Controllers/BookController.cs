
using System;
using System.Linq;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.DeleteBook;
using WebApi.BookOperations.GetBookDetail;
using WebApi.BookOperations.GetBooks;
using WebApi.BookOperations.UpdateBook;
using WebApi.DBOperations;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]s")]
     public class BookController : ControllerBase
     {
         private readonly BookStoreDbContext _context;
         private readonly IMapper _mapper;

         public BookController(BookStoreDbContext context,IMapper mapper){
             _context = context;
             _mapper = mapper;
         }
         
      

         [HttpGet]

         public IActionResult GetBooks(){

             GetBooksQuery query = new GetBooksQuery(_context,_mapper);
             var result = query.Handle();
             return Ok(result);

         }

         [HttpGet("{id}")]

         public IActionResult GetById (int id)
         {

             BookDetailViewModel result;

             try
             {
                 var query = new GetBookDetailQuery(_context,_mapper);
                 query.BookId = id;
                 var validator = new GetBookDetailQueryValidation();
                 validator.ValidateAndThrow(query);
                 result = query.Handle();
             }
             catch(Exception ex)
             {
                 return BadRequest(ex.Message);
             }
            return Ok(result);
         }

         [HttpPost]

         public IActionResult AddBook([FromBody] CreateBookModel newBook)
         {
             CreateBookCommand command = new CreateBookCommand(_context,_mapper);

             try{
                 command.Model = newBook;
                 var validator = new CreateBookCommandValidator();
                 validator.ValidateAndThrow(command);
                 command.Handle();
             }
             catch(Exception ex)
             {
                 return BadRequest(ex.Message);
             }
             
             
             return Ok();
         }

         [HttpPut("{id}")]

         public IActionResult UpdateBook(int id ,[FromBody] UpdateBookModel updatedBook)
         {
             
             try
             {
                 var command = new UpdatedBookCommand(_context);
                 command.BookId = id;
                 command.Model =updatedBook;
                 var validator = new UpdatedBookCommandValidation();
                 validator.ValidateAndThrow(command);
                 command.Handle();
             }
             catch(Exception ex)
             {
                 return BadRequest(ex.Message);
             }
             return Ok();
         }

         [HttpDelete("{id}")]

         public IActionResult DeleteBook(int id)
         {
             try
             {
                 var command = new DeleteBookCommand(_context);
                 command.BookId = id;
                 var validator = new DeleteBookCommandValidation();
                 validator.ValidateAndThrow(command);
                 command.Handle();
             }
             catch(Exception ex)
             {
                 return BadRequest(ex.Message);
             }
             
             return Ok();
         }
     }
 }