using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.DataTransferObjects;
using Entities.Models;

namespace Services.Contracts
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllCategoriesAsync(bool tracking);
        Task<Category> GetOneCategoryByIdAsync(int id, bool tracking);
        Task CreateOneCategoryAsync(CategoryForInsertionDto categoryForInsertionDto);
        Task UpdateOneCategoryAsync(int id, CategoryForUpdateDto categoryForUpdateDto, bool tracking);
        Task DeleteOneCategoryAsync(int id, bool tracking);
    }
}