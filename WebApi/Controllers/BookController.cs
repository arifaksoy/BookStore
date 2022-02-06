
using System;
using System.Linq;
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

         public BookController(BookStoreDbContext context){
             _context = context;
         }
         
      

         [HttpGet]

         public IActionResult GetBooks(){

             GetBooksQuery query = new GetBooksQuery(_context);
             var result = query.Handle();
             return Ok(result);

         }

         [HttpGet("{id}")]

         public IActionResult GetById (int id)
         {

             BookDetailViewModel result;

             try
             {
                 var query = new GetBookDetailQuery(_context);
                 query.BookId = id;
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
             CreateBookCommand command = new CreateBookCommand(_context);

             try{
                 command.Model = newBook;
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