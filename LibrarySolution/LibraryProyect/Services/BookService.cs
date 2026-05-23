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
        private readonly ICategoryRepository _categoryRepository;

        public BookService(
            IBookRepository bookRepository,
            IAuthorRepository authorRepository,
            ICategoryRepository categoryRepository)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await _bookRepository.GetAllAsync();
        }

        public async Task<Book?> GetByIdAsync(int id)
        {
            return await _bookRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Book>> GetByAuthorIdAsync(int authorId)
        {
            return await _bookRepository.GetByAuthorIdAsync(authorId);
        }

        public async Task<IEnumerable<Book>> GetByCategoryIdAsync(int categoryId)
        {
            return await _bookRepository.GetByCategoryIdAsync(categoryId);
        }

        public async Task<Book?> GetByIsbnAsync(string isbn)
        {
            return await _bookRepository.GetByIsbnAsync(isbn);
        }

        public async Task<Book> AddAsync(Book book)
        {
            // Validación: título obligatorio
            if (string.IsNullOrWhiteSpace(book.Title))
                throw new ArgumentException("El título del libro es obligatorio.");

            // Validación: ISBN obligatorio y único
            if (string.IsNullOrWhiteSpace(book.ISBN))
                throw new ArgumentException("El ISBN es obligatorio.");

            var existing = await _bookRepository.GetByIsbnAsync(book.ISBN);
            if (existing != null)
                throw new ArgumentException("El ISBN ya existe.");

            // Validación: año de publicación válido
            if (book.PublishedYear <= 0 || book.PublishedYear > DateTime.Now.Year)
                throw new ArgumentException("El año de publicación es inválido.");

            // Validación: autor existente
            var author = await _authorRepository.GetByIdAsync(book.AuthorId);
            if (author == null)
                throw new ArgumentException("El autor especificado no existe.");

            return await _bookRepository.AddAsync(book);
        }

        public async Task<bool> UpdateAsync(Book book)
        {
            // Validación: título obligatorio
            if (string.IsNullOrWhiteSpace(book.Title))
                throw new ArgumentException("El título del libro es obligatorio.");

            // Validación: ISBN obligatorio y único (excepto el mismo libro)
            if (string.IsNullOrWhiteSpace(book.ISBN))
                throw new ArgumentException("El ISBN es obligatorio.");

            var existing = await _bookRepository.GetByIsbnAsync(book.ISBN);
            if (existing != null && existing.Id != book.Id)
                throw new ArgumentException("El ISBN ya está registrado por otro libro.");

            // Validación: año de publicación válido
            if (book.PublishedYear <= 0 || book.PublishedYear > DateTime.Now.Year)
                throw new ArgumentException("El año de publicación es inválido.");

            // Validación: autor existente
            var author = await _authorRepository.GetByIdAsync(book.AuthorId);
            if (author == null)
                throw new ArgumentException("El autor especificado no existe.");

            return await _bookRepository.UpdateAsync(book);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _bookRepository.DeleteAsync(id);
        }
    }
}
