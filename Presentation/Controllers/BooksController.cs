using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using Microsoft.AspNetCore.JsonPatch;
using Entities.Exceptions;
using Entities.DataTransferObjects;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public BooksController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet]
        public IActionResult GetAllBooks()
        {
                var books = _serviceManager.BookService.GetAllBooks(false);
                return Ok(books);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetOneBook([FromRoute(Name = "id")] int id)
        {
                 var book = _serviceManager.BookService.GetOneBookById(id, false);
                 return Ok(book);
        }

        [HttpPost]
        public IActionResult CreateOneBook([FromBody] Book book)
        {
                if(book is null)
                    return BadRequest();
                _serviceManager.BookService.CreateOneBook(book);
                return StatusCode(201, book);
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateOneBook([FromRoute(Name = "id")] int id, [FromBody] BookForUpdateDto bookForUpdateDto)
        {
            //check book? 
                if(bookForUpdateDto is null)
                    return BadRequest();
                _serviceManager.BookService.UpdateOneBook(id, bookForUpdateDto, true);
                return NoContent();
            
            
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteOneBook([FromRoute(Name = "id")] int id)
        {
                _serviceManager.BookService.DeleteOneBook(id, false);
                return NoContent(); // 204 
        }

        [HttpPatch("{id:int}")]
        public IActionResult PartiallyUpdateOneBook([FromRoute(Name = "id")] int id, [FromBody] JsonPatchDocument<Book> bookPatch)
        {
            // check entity
                var entity = _serviceManager.BookService.GetOneBookById(id, true);
                if(entity is null)
                   throw new BookNotFoundException(id); // 404

                bookPatch.ApplyTo(entity);
                _serviceManager.BookService.UpdateOneBook(id, new(entity.Id,entity.Title, entity.Price), true);
               
                return NoContent(); // 204 No Content
        }
    }

}