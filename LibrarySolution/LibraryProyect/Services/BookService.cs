using Library.Domain.Enities;
using Library.Domain.Entities;
using Library.Domain.Interfaces.Repositories;
using Library.Domain.Interfaces.Services;

namespace LibraryProyect.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _repository;
        private readonly IAuthorRepository _authorRepository;

        public BookService(IBookRepository repository, IAuthorRepository authorRepository)
        {
            _repository = repository;
            _authorRepository = authorRepository;
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Book?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Book> CreateAsync(Book book)
        {
            if (string.IsNullOrWhiteSpace(book.Title))
                throw new Exception("El título del libro es obligatorio.");

            var existingBooks = await _repository.GetAllAsync();
            if (existingBooks.Any(b => b.ISBN == book.ISBN))
                throw new Exception("Ya existe un libro con ese ISBN.");

            if (book.PublishedYear > DateTime.Now.Year)
                throw new Exception("El año de publicación no puede ser mayor al actual.");

            var author = await _authorRepository.GetByIdAsync(book.AuthorId);
            if (author == null)
                throw new Exception("El autor no existe.");

            await _repository.AddAsync(book);
            return book;
        }

        public async Task<bool> UpdateAsync(int id, Book book)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return false;

            existing.Title = book.Title;
            existing.ISBN = book.ISBN;
            existing.PublishedYear = book.PublishedYear;
            existing.AuthorId = book.AuthorId;

            await _repository.UpdateAsync(existing);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return false;

            await _repository.DeleteAsync(id);
            return true;
        }
    }
}
