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

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ServiceFilter(typeof(LogFilterAttritbute))]
    public class BooksController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public BooksController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet]
        [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
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

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetOneBookAsync([FromRoute(Name = "id")] int id)
        {
                 var book = await _serviceManager.BookService.GetOneBookByIdAsync(id, false);
                 return Ok(book);
        }

        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPost]
        public async Task<IActionResult> CreateOneBookAsync([FromBody] BookForInsertionDto bookForInsertion)
        {
                await _serviceManager.BookService.CreateOneBookAsync(bookForInsertion);
                return StatusCode(201, bookForInsertion);
        }

        
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateOneBookAsync([FromRoute(Name = "id")] int id, [FromBody] BookForUpdateDto bookForUpdateDto)
        {
            //check book? 
                await _serviceManager.BookService.UpdateOneBookAsync(id, bookForUpdateDto, false);
                return NoContent(); 
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteOneBookAsync([FromRoute(Name = "id")] int id)
        {
                await _serviceManager.BookService.DeleteOneBookAsync(id, false);
                return NoContent(); // 204 
        }

        [HttpPatch("{id:int}")]
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
    }

}