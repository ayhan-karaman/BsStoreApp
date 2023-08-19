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
        Task<IEnumerable<BookDto>> GetAllBooksAsync(bool tracking);
        Task<BookDto> GetOneBookByIdAsync(int id, bool tracking);
        Task<BookDto> CreateOneBookAsync(BookForInsertionDto book);
        Task UpdateOneBookAsync(int id, BookForUpdateDto bookForUpdateDto, bool tracking);
        Task DeleteOneBookAsync(int id, bool tracking);
        Task<(BookForUpdateDto bookForUpdate, Book book)> GetOneBookForPatchAsync(int id, bool tracking);

        Task SaveChangesForPatchAsync(BookForUpdateDto bookForUpdateDto, Book book);
    }
}