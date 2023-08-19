using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Models;

namespace Repositories.Contracts
{
    public interface IBookRepository : IRepositoryBase<Book>
    {
        Task<IEnumerable<Book>> GetAllBooksAsync(bool tracking);
        Task<Book> GetOneBookAsync(int id, bool tracking);
        void CreateOneBook(Book book);
        void DeleteOneBook(Book book);
        void UpdateOneBook(Book book);
    }
}