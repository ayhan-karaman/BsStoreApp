using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using Repositories.Extensions;

namespace Repositories.EfCore
{
    public sealed class BookRepository : RepositoryBase<Book>, IBookRepository
    {
        public BookRepository(RepositoryContext context) : base(context)
        {
        }

        public void CreateOneBook(Book book) => Create(book);

        public void DeleteOneBook(Book book) => Delete(book);

        public async Task<PagedList<Book>> GetAllBooksAsync(BookParameters bookParameters,  bool tracking) 
        {
            var books =  await FindAll(tracking)
            .FilterBooks(bookParameters.MinPrice, bookParameters.MaxPrice)
            .Search(bookParameters.SearchTerm)
            .Sort(bookParameters.OrderBy)
            .ToListAsync();
            return PagedList<Book>.ToPagedList(books, bookParameters.PageNumber, bookParameters.PageSize);
        }

        public async Task<List<Book>> GetAllBooksAsync(bool tracking) 
        => await FindAll(tracking).OrderBy(b => b.Id).ToListAsync();
          
        
        public async Task<Book> GetOneBookAsync(int id, bool tracking) => await FindByCondition(b => b.Id.Equals(id), tracking).SingleOrDefaultAsync();

        public void UpdateOneBook(Book book)
        {
            throw new NotImplementedException();
        }
    }
}