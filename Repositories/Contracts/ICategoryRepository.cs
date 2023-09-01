using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Models;
using Entities.RequestFeatures;

namespace Repositories.Contracts
{
    public interface ICategoryRepository : IRepositoryBase<Category>
    {
        Task<IEnumerable<Category>> GetAllCategoriesAsync(bool tracking);
        Task<Category> GetOneCategoryByIdAsync(int id, bool tracking);
        void CreateOneCategory(Category category);
        void UpdateOneCategory(Category category);
        void DeleteOneCategory(Category category);
    }
}