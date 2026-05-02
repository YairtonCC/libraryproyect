using Library.Domain.Enities.Library.Domain.Entities;
using Library.Domain.Entities;
using Library.Domain.Interfaces.Repositories;
using Library.Domain.Interfaces.Services;

namespace LibraryProyect.Services
{
    public class BookCategoryService : IBookCategoryService
    {
        private readonly IBookCategoryRepository _repository;

        public BookCategoryService(IBookCategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<BookCategory>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<BookCategory?> GetByIdsAsync(int bookId, int categoryId)
        {
            return await _repository.GetByIdsAsync(bookId, categoryId);
        }

        public async Task<BookCategory> CreateAsync(BookCategory bookCategory)
        {
            var existing = await _repository.GetByIdsAsync(bookCategory.BookId, bookCategory.CategoryId);
            if (existing != null)
                throw new Exception("El libro ya está asignado a esta categoría.");

            await _repository.AddAsync(bookCategory);
            return bookCategory;
        }

        public async Task<bool> DeleteAsync(int bookId, int categoryId)
        {
            var existing = await _repository.GetByIdsAsync(bookId, categoryId);
            if (existing == null) return false;

            await _repository.DeleteAsync(bookId, categoryId);
            return true;
        }
    }
}
