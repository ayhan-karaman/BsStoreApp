using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using Entities.DataTransferObjects;
using Entities.LinkModels;
using Entities.Models;
using Entities.RequestFeatures;

namespace Services.Contracts
{
    public interface IBookService
    {
        Task<(LinkResponse linkResponse, MetaData metaData)> GetAllBooksAsync(LinkParameters linkParameters, bool tracking);
        Task<BookDto> GetOneBookByIdAsync(int id, bool tracking);
        Task<BookDto> CreateOneBookAsync(BookForInsertionDto book);
        Task UpdateOneBookAsync(int id, BookForUpdateDto bookForUpdateDto, bool tracking);
        Task DeleteOneBookAsync(int id, bool tracking);
        Task<(BookForUpdateDto bookForUpdate, Book book)> GetOneBookForPatchAsync(int id, bool tracking);

        Task SaveChangesForPatchAsync(BookForUpdateDto bookForUpdateDto, Book book);
        Task<List<Book>> GetAllBooksAsync(bool tracking);
        Task<IEnumerable<Book>> GetAllBooksWithDetailAsync(bool tracking);
    }
}