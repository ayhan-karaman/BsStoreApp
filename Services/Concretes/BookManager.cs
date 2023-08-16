using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public BookManager(IRepositoryManager repositoryManager, ILoggerService loggerService)
        {
            _repositoryManager = repositoryManager;
            _loggerService = loggerService;
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

        public IEnumerable<Book> GetAllBooks(bool tracking)
        {
            return _repositoryManager.Book.GetAllBooks(tracking);
        }

        public Book GetOneBookById(int id, bool tracking)
        {
             var book =  _repositoryManager.Book.GetOneBook(id, tracking);
             if(book is null)
                throw new BookNotFoundException(id);
             return book;
        }

        public void UpdateOneBook(int id, Book book, bool tracking)
        {
            var entity = _repositoryManager.Book.GetOneBook(id, tracking);
            if(entity is  null)
                throw new BookNotFoundException(id);
            
            // check params
            entity.Title = book.Title;
            entity.Price = book.Price;

            _repositoryManager.Book.Update(entity);
            _repositoryManager.Save();
        }
    }
}