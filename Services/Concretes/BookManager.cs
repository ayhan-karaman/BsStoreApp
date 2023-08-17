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

        public Book CreateOneBook(Book book)
        {
            _repositoryManager.Book.CreateOneBook(book);
            _repositoryManager.Save();
            return book;
        }

        public void DeleteOneBook(int id, bool tracking)
        {
            var entity = _repositoryManager.Book.GetOneBook(id, tracking);
            if(entity is  null)
                throw new BookNotFoundException(id);

            _repositoryManager.Book.DeleteOneBook(entity);
            _repositoryManager.Save();
        }

        public IEnumerable<BookDto> GetAllBooks(bool tracking)
        {
            var books =  _repositoryManager.Book.GetAllBooks(tracking);
            return _mapper.Map<IEnumerable<BookDto>>(books);
        }

        public Book GetOneBookById(int id, bool tracking)
        {
             var book =  _repositoryManager.Book.GetOneBook(id, tracking);
             if(book is null)
                throw new BookNotFoundException(id);
             return book;
        }

        public void UpdateOneBook(int id, BookForUpdateDto bookForUpdateDto, bool tracking)
        {
            var entity = _repositoryManager.Book.GetOneBook(id, tracking);
            if(entity is  null)
                throw new BookNotFoundException(id);
            
            // Mapping
            entity = _mapper.Map<Book>(bookForUpdateDto);

            _repositoryManager.Book.Update(entity);
            _repositoryManager.Save();
        }
    }
}