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
        IEnumerable<BookDto> GetAllBooks(bool tracking);
        BookDto GetOneBookById(int id, bool tracking);
        BookDto CreateOneBook(BookForInsertionDto book);
        void UpdateOneBook(int id, BookForUpdateDto bookForUpdateDto, bool tracking);
        void DeleteOneBook(int id, bool tracking);
        (BookForUpdateDto bookForUpdate, Book book) GetOneBookForPatch(int id, bool tracking);

        void SaveChangesForPatch(BookForUpdateDto bookForUpdateDto, Book book);
    }
}