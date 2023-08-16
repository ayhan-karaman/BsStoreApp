using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.DataTransferObjects;
using Entities.Models;

namespace Services.Contracts
{
    public interface IBookService
    {
        IEnumerable<Book> GetAllBooks(bool tracking);
        Book GetOneBookById(int id, bool tracking);
        Book CreateOneBook(Book book);
        void UpdateOneBook(int id, BookForUpdateDto bookForUpdateDto, bool tracking);
        void DeleteOneBook(int id, bool tracking);
    }
}