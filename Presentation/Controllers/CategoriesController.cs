using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilters;
using Services.Contracts;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public CategoriesController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
             var categories = await _serviceManager.CategoryService.GetAllCategoriesAsync(false);
             return Ok(categories);
        }

        [HttpGet("{categoryId:int}")]
        public async Task<IActionResult> GetOneCategory([FromRoute] int categoryId)
        {
            var category = await _serviceManager.CategoryService.GetOneCategoryByIdAsync(categoryId, false);
            return Ok(category);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateOneCategory([FromBody] CategoryForInsertionDto categoryForInsertion)
        {
           await  _serviceManager.CategoryService.CreateOneCategoryAsync(categoryForInsertion);
            return Ok(new{
                 message = "Kategori eklendi"
            });
        }

        [HttpPut("{id:int}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateOneCategory([FromRoute(Name = "id")] int id, [FromBody] CategoryForUpdateDto categoryForUpdateDto)
        {
            await _serviceManager.CategoryService.UpdateOneCategoryAsync(id, categoryForUpdateDto, false);
            return Ok(new{
                 message = "Kategori güncellendi"
            });
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteOneCategory([FromRoute(Name = "id")] int id)
        {
                await _serviceManager.CategoryService.DeleteOneCategoryAsync(id, false);
                return Ok(new{
                 message = "Kategori silindi"
                });
        }
    }
}