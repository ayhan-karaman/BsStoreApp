using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;

namespace Repositories.EfCore
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(RepositoryContext context) : base(context)
        {
        }

        public void CreateOneCategory(Category category) => Create(category);

        public void DeleteOneCategory(Category category) => Delete(category);

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync(bool tracking)
        {
            return await FindAll(tracking)
                    .OrderBy(x => x.CategoryName)
                    .ToListAsync();
        }

        public async Task<Category> GetOneCategoryByIdAsync(int id, bool tracking)
        {
            Category? category = await FindByCondition(x => x.Id.Equals(id), tracking).SingleOrDefaultAsync();
            return category;
        }

        public void UpdateOneCategory(Category category) => Update(category);
    }
}