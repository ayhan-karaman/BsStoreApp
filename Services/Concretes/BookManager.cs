using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;

namespace Services.Concretes
{
    public class BookManager : IBookService
    {
        private readonly IRepositoryManager _repositoryManager;

        public BookManager(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
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
               throw new Exception($"Book with id:{id} could not found");

            _repositoryManager.Book.DeleteOneBook(entity);
            _repositoryManager.Save();
        }

        public IEnumerable<Book> GetAllBooks(bool tracking)
        {
            return _repositoryManager.Book.GetAllBooks(tracking);
        }

        public Book GetOneBookById(int id, bool tracking)
        {
             return _repositoryManager.Book.GetOneBook(id, tracking);
        }

        public void UpdateOneBook(int id, Book book, bool tracking)
        {
            var entity = _repositoryManager.Book.GetOneBook(id, tracking);
            if(entity is  null)
               throw new Exception($"Book with id:{id} could not found");
            // check params
            if(book is null)
              throw new ArgumentNullException(nameof(book));
            entity.Title = book.Title;
            entity.Price = book.Price;

            _repositoryManager.Book.Update(entity);
            _repositoryManager.Save();
        }
    }
}