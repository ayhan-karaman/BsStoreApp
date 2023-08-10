using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Entities.Models;
using Repositories.EfCore;

namespace WebApiUI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly RepositoryContext _repositoryContext;

        public BooksController(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        [HttpGet]
        public IActionResult GetAllBooks()
        {
            try
            {
                var books = _repositoryContext.Books.ToList();
                return Ok(books);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("{id:int}")]
        public IActionResult GetOneBook([FromRoute(Name = "id")] int id)
        {
            try
            {
                 var book = _repositoryContext.Books.Where(book => book.Id == id).SingleOrDefault();
                 if(book is null)
                    return NotFound(); //404
                 return Ok(book);
            }
            catch (Exception ex)
            {
                
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult CreateOneBook([FromBody] Book book)
        {
            try
            {
                if(book is null)
                    return BadRequest();
                _repositoryContext.Books.Add(book);
                _repositoryContext.SaveChanges();
                return StatusCode(201, book);
            }
            catch (Exception ex)
            {
                
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateOneBook([FromRoute(Name = "id")] int id, [FromBody] Book book)
        {
            //check book? 
            try
            {
                var entity = _repositoryContext.Books.Where(b => b.Id.Equals(id)).SingleOrDefault();
                if(entity is null)
                     return NotFound();
                // check id
                if(id != book.Id)
                    return BadRequest(); // 404

                entity.Title = book.Title;
                entity.Price = book.Price;

                _repositoryContext.SaveChanges();
                return Ok(entity);
            }
            catch (Exception ex)
            {
                
                throw new Exception(ex.Message);
            }
            
            
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteOneBook([FromRoute(Name = "id")] int id)
        {
            try
            {
                var entity = _repositoryContext.Books.Where(b => b.Id.Equals(id)).SingleOrDefault();
                if(entity is null)
                    return NotFound(new {
                         statusCode = 404,
                         message = $"Book with id:{id} could not found"
                    }); // 404 
                _repositoryContext.Books.Remove(entity);
                _repositoryContext.SaveChanges();
                return NoContent(); // 204 
            }
            catch (Exception ex)
            {
                
                throw new Exception(ex.Message);
            }
        }

        [HttpPatch("{id:int}")]
        public IActionResult PartiallyUpdateOneBook([FromRoute(Name = "id")] int id, [FromBody] JsonPatchDocument<Book> bookPatch)
        {
             try
             {
                // check entity
                var entity = _repositoryContext.Books.Where(b => b.Id.Equals(id)).SingleOrDefault();
                if(entity is null)
                   return NotFound(); // 404
                  bookPatch.ApplyTo(entity);
                _repositoryContext.SaveChanges();
                return NoContent(); // 204 No Content
             }
             catch (Exception ex)
            {
                
                throw new Exception(ex.Message);
            }
        }
    }
}