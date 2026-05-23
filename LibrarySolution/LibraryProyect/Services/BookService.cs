using Library.Domain.Enities;
using Library.Domain.Entities;
using Library.Domain.Interfaces.Repositories;
using Library.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryProyect.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;

        public BookService(IBookRepository bookRepository, IAuthorRepository authorRepository)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await _bookRepository.GetAllAsync();
        }

        public async Task<Book?> GetByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID del libro debe ser mayor que cero.");

            return await _bookRepository.GetByIdAsync(id);
        }

        public async Task<Book> AddAsync(Book book)
        {
            if (string.IsNullOrWhiteSpace(book.Title))
                throw new ArgumentException("El título del libro es obligatorio.");

            if (string.IsNullOrWhiteSpace(book.ISBN))
                throw new ArgumentException("El ISBN es obligatorio.");

            // Validar ISBN único
            var existingBooks = await _bookRepository.GetAllAsync();
            foreach (var b in existingBooks)
            {
                if (b.ISBN.Equals(book.ISBN, StringComparison.OrdinalIgnoreCase))
                    throw new ArgumentException("Ya existe un libro con ese ISBN.");
            }

            // Validar año de publicación
            if (book.PublishedYear < 1500 || book.PublishedYear > DateTime.Now.Year)
                throw new ArgumentException("El año de publicación es inválido.");

            // Validar que el autor exista
            var author = await _authorRepository.GetByIdAsync(book.AuthorId);
            if (author == null)
                throw new ArgumentException("El autor especificado no existe.");

            return await _bookRepository.AddAsync(book);
        }

        public async Task<bool> UpdateAsync(Book book)
        {
            if (book.Id <= 0)
                throw new ArgumentException("El ID del libro es inválido.");

            if (string.IsNullOrWhiteSpace(book.Title))
                throw new ArgumentException("El título del libro es obligatorio.");

            if (string.IsNullOrWhiteSpace(book.ISBN))
                throw new ArgumentException("El ISBN es obligatorio.");

            if (book.PublishedYear < 1500 || book.PublishedYear > DateTime.Now.Year)
                throw new ArgumentException("El año de publicación es inválido.");

            return await _bookRepository.UpdateAsync(book);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID del libro debe ser mayor que cero.");

            return await _bookRepository.DeleteAsync(id);
        }
    }
}
