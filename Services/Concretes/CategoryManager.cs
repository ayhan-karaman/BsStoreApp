using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Exceptions;
using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;

namespace Services.Concretes
{
    public class CategoryManager : ICategoryService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public CategoryManager(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task CreateOneCategoryAsync(CategoryForInsertionDto categoryForInsertionDto)
        {
            var category = _mapper.Map<Category>(categoryForInsertionDto);
             _repositoryManager.Category.CreateOneCategory(category);
             await _repositoryManager.SaveAsync();
             
        }

        public async Task DeleteOneCategoryAsync(int id, bool tracking)
        {
            var category = await GetOneCategoryAndCheckExists(id, tracking);

            _repositoryManager.Category.DeleteOneCategory(category);
            await _repositoryManager.SaveAsync();
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync(bool tracking)
        {
            return await _repositoryManager.Category.GetAllCategoriesAsync(tracking);
        }

        public async Task<Category> GetOneCategoryByIdAsync(int id, bool tracking)
        {
            var category =  await _repositoryManager.Category.GetOneCategoryByIdAsync(id, tracking);
            if(category is null)
                 throw new CategoryNotFoundException(id);
                 
            return category;
        }

        public async Task UpdateOneCategoryAsync(int id, CategoryForUpdateDto categoryForUpdateDto, bool tracking)
        {
            var category = await GetOneCategoryAndCheckExists(id, tracking);
            category = _mapper.Map<Category>(categoryForUpdateDto);

            _repositoryManager.Category.UpdateOneCategory(category);
            await _repositoryManager.SaveAsync();
        }

        private async Task<Category> GetOneCategoryAndCheckExists(int id, bool tracking)
        {
             var entity = await _repositoryManager.Category.GetOneCategoryByIdAsync(id, tracking);
             if(entity is null)
                throw new CategoryNotFoundException(id);
            return entity;
        }
    }
}