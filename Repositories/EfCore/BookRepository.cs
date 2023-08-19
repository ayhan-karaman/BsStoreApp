using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;

namespace Repositories.EfCore
{
    public class BookRepository : RepositoryBase<Book>, IBookRepository
    {
        public BookRepository(RepositoryContext context) : base(context)
        {
        }

        public void CreateOneBook(Book book) => Create(book);

        public void DeleteOneBook(Book book) => Delete(book);

        public async Task<IEnumerable<Book>> GetAllBooksAsync(bool tracking) => await FindAll(tracking).OrderBy(x => x.Id).ToListAsync();

        public async Task<Book> GetOneBookAsync(int id, bool tracking) => await FindByCondition(b => b.Id.Equals(id), tracking).SingleOrDefaultAsync();

        public void UpdateOneBook(Book book)
        {
            throw new NotImplementedException();
        }
    }
}