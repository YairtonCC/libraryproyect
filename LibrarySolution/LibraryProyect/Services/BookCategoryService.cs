using Library.Domain.Interfaces.Repositories;
using Library.Domain.Interfaces.Services;

namespace LibraryProyect.Services
{
    public class BookCategoryService : IBookCategoryService
    {
        private readonly IBookCategoryRepository _bookCategoryRepository;

        public BookCategoryService(IBookCategoryRepository bookCategoryRepository)
        {
            _bookCategoryRepository = bookCategoryRepository;
        }

        public async Task<IEnumerable<BookCategory>> GetAllAsync()
        {
            return await _bookCategoryRepository.GetAllAsync();
        }

        public async Task<BookCategory?> GetByIdAsync(int id)
        {
            return await _bookCategoryRepository.GetByIdAsync(id);
        }

        public async Task<BookCategory> AddAsync(BookCategory bookCategory)
        {
            return await _bookCategoryRepository.AddAsync(bookCategory);
        }

        public async Task<bool> UpdateAsync(BookCategory bookCategory)
        {
            return await _bookCategoryRepository.UpdateAsync(bookCategory);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _bookCategoryRepository.DeleteAsync(id);
        }
    }
}
