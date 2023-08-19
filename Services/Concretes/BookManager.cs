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
    public class BookManager : IBookService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILoggerService _loggerService;
        private readonly IMapper _mapper;

        public BookManager(IRepositoryManager repositoryManager, ILoggerService loggerService, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _loggerService = loggerService;
            _mapper = mapper;
        }

        public async Task<BookDto> CreateOneBookAsync(BookForInsertionDto bookForInsertion)
        {
            var entity = _mapper.Map<Book>(bookForInsertion);
            _repositoryManager.Book.CreateOneBook(entity);
            await  _repositoryManager.SaveAsync();

            return _mapper.Map<BookDto>(entity);
        }

        public async Task DeleteOneBookAsync(int id, bool tracking)
        {
            var entity = await _repositoryManager.Book.GetOneBookAsync(id, tracking);
            if(entity is  null)
                throw new BookNotFoundException(id);

            _repositoryManager.Book.DeleteOneBook(entity);
            await _repositoryManager.SaveAsync();
        }

        public async Task<IEnumerable<BookDto>> GetAllBooksAsync(bool tracking)
        {
            var books = await  _repositoryManager.Book.GetAllBooksAsync(tracking);
            return _mapper.Map<IEnumerable<BookDto>>(books);
        }

        public async Task<BookDto> GetOneBookByIdAsync(int id, bool tracking)
        {
             var book = await  _repositoryManager.Book.GetOneBookAsync(id, tracking);
             if(book is null)
                throw new BookNotFoundException(id);
             var bookDto = _mapper.Map<BookDto>(book);
             return bookDto;
        }

        public async Task<(BookForUpdateDto bookForUpdate, Book book)> GetOneBookForPatchAsync(int id, bool tracking)
        {
            var book = await _repositoryManager.Book.GetOneBookAsync(id, tracking);
            if(book is null)
                throw new BookNotFoundException(id);
            var bookDto = _mapper.Map<BookForUpdateDto>(book);
            return (bookDto, book);
        }

        public async Task SaveChangesForPatchAsync(BookForUpdateDto bookForUpdateDto, Book book)
        {
            _mapper.Map(bookForUpdateDto, book);
            await _repositoryManager.SaveAsync();
        }

        public async Task UpdateOneBookAsync(int id, BookForUpdateDto bookForUpdateDto, bool tracking)
        {
            var entity = await _repositoryManager.Book.GetOneBookAsync(id, tracking);
            if(entity is  null)
                throw new BookNotFoundException(id);
            
            // Mapping
            entity = _mapper.Map<Book>(bookForUpdateDto);

             _repositoryManager.Book.Update(entity);
            await  _repositoryManager.SaveAsync();
        }
    }
}