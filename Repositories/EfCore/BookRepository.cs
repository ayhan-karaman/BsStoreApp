using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Models;
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

        public IQueryable<Book> GetAllBooks(bool tracking) => FindAll(tracking);

        public IQueryable<Book> GetByIdBook(int id, bool tracking) => FindByCondition(b => b.Id.Equals(id), tracking);

        public void UpdateOneBook(Book book)
        {
            throw new NotImplementedException();
        }
    }
}