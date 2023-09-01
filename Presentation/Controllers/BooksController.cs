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
using Presentation.ActionFilters;
using Entities.RequestFeatures;
using System.Text.Json;
using Marvin.Cache.Headers;
using Microsoft.AspNetCore.Authorization;

namespace Presentation.Controllers
{
    // [ApiVersion("1.0")]
    [ApiController]
     [Route("api/books")]
    [ServiceFilter(typeof(LogFilterAttritbute))]
    // [ResponseCache(CacheProfileName = "5mins")]
    // [HttpCacheExpiration(CacheLocation = CacheLocation.Public, MaxAge = 80)]
    public class BooksController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public BooksController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpHead]
        [HttpGet]
        [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
        [Authorize]
        [ApiExplorerSettings(GroupName = "v1")]
        //[ResponseCache(Duration = 60)] // Cache'lenebilir yapıyı kazandırır
        public async Task<IActionResult> GetAllBooksAsync([FromQuery] BookParameters bookParameters)
        {
                var linkParameters = new LinkParameters()
                {
                     BookParameters = bookParameters,
                     HttpContext = HttpContext
                };
                var result = await _serviceManager.BookService.GetAllBooksAsync(linkParameters, false);
                Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(result.metaData));
                return result.linkResponse.HasLinks ? Ok(result.linkResponse.LinkedEntities) : Ok(result.linkResponse.ShapedEntities);
        }

        [Authorize]
        [HttpGet("details")]
        public async Task<IActionResult> GetAllBooksWithDetailAsync()
        {
            var books = await _serviceManager.BookService.GetAllBooksWithDetailAsync(false);
            return Ok(books);
        }

        [HttpGet("{id:int}")]
        [Authorize(Roles = "Admin, Editor")]
        public async Task<IActionResult> GetOneBookAsync([FromRoute(Name = "id")] int id)
        {
                 var book = await _serviceManager.BookService.GetOneBookByIdAsync(id, false);
                 return Ok(book);
        }

        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPost(Name = "CreateOneBookAsync")]
        [Authorize(Roles = "Admin, Editor")]
        public async Task<IActionResult> CreateOneBookAsync([FromBody] BookForInsertionDto bookForInsertion)
        {
                await _serviceManager.BookService.CreateOneBookAsync(bookForInsertion);
                return StatusCode(201, bookForInsertion);
        }

        
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin, Editor")]
        public async Task<IActionResult> UpdateOneBookAsync([FromRoute(Name = "id")] int id, [FromBody] BookForUpdateDto bookForUpdateDto)
        {
            //check book? 
                await _serviceManager.BookService.UpdateOneBookAsync(id, bookForUpdateDto, false);
                return NoContent(); 
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteOneBookAsync([FromRoute(Name = "id")] int id)
        {
                await _serviceManager.BookService.DeleteOneBookAsync(id, false);
                return NoContent(); // 204 
        }

        [HttpPatch("{id:int}")]
        [Authorize(Roles = "Admin, Editor")]
        public async Task<IActionResult> PartiallyUpdateOneBookAsync([FromRoute(Name = "id")] int id, [FromBody] JsonPatchDocument<BookForUpdateDto> bookPatch)
        {
            if(bookPatch is null)
                return BadRequest(); // 400
            
            var result = await _serviceManager.BookService.GetOneBookForPatchAsync(id, false);

            bookPatch.ApplyTo(result.bookForUpdate, ModelState);

            TryValidateModel(result.bookForUpdate);

            if(!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            await _serviceManager.BookService.SaveChangesForPatchAsync(result.bookForUpdate, result.book);
            
            return NoContent(); // 204 No Content
        }
    
        [HttpOptions]
        [Authorize]
        public IActionResult GetBooksOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE, PATCH, HEAD, OPTIONS");
            return Ok();
        }
    
      
        
    }

}