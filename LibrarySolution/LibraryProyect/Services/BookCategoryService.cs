using Library.Domain.Interfaces.Repositories;
using Library.Domain.Interfaces.Services;


namespace LibraryProyect.Services
{
    public class BookCategoryService : IBookCategoryService
    {
        private readonly IBookCategoryRepository _bookCategoryRepository;
        private readonly IBookRepository _bookRepository;
        private readonly ICategoryRepository _categoryRepository;

        public BookCategoryService(
            IBookCategoryRepository bookCategoryRepository,
            IBookRepository bookRepository,
            ICategoryRepository categoryRepository)
        {
            _bookCategoryRepository = bookCategoryRepository;
            _bookRepository = bookRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<BookCategory>> GetAllAsync()
        {
            return await _bookCategoryRepository.GetAllAsync();
        }

        public async Task<BookCategory?> GetByIdAsync(int id)
        {
            return await _bookCategoryRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<BookCategory>> GetByBookIdAsync(int bookId)
        {
            return await _bookCategoryRepository.GetByBookIdAsync(bookId);
        }

        public async Task<IEnumerable<BookCategory>> GetByCategoryIdAsync(int categoryId)
        {
            return await _bookCategoryRepository.GetByCategoryIdAsync(categoryId);
        }

        public async Task<BookCategory> AddAsync(BookCategory bookCategory)
        {
            // Validación: libro existente
            var book = await _bookRepository.GetByIdAsync(bookCategory.BookId);
            if (book == null)
                throw new ArgumentException("El libro especificado no existe.");

            // Validación: categoría existente
            var category = await _categoryRepository.GetByIdAsync(bookCategory.CategoryId);
            if (category == null)
                throw new ArgumentException("La categoría especificada no existe.");

            // Validación: evitar duplicados
            var existingRelations = await _bookCategoryRepository.GetByBookIdAsync(bookCategory.BookId);
            foreach (var relation in existingRelations)
            {
                if (relation.CategoryId == bookCategory.CategoryId)
                    throw new ArgumentException("La relación libro‑categoría ya existe.");
            }

            return await _bookCategoryRepository.AddAsync(bookCategory);
        }

        public async Task<bool> UpdateAsync(BookCategory bookCategory)
        {
            // Validación: libro existente
            var book = await _bookRepository.GetByIdAsync(bookCategory.BookId);
            if (book == null)
                throw new ArgumentException("El libro especificado no existe.");

            // Validación: categoría existente
            var category = await _categoryRepository.GetByIdAsync(bookCategory.CategoryId);
            if (category == null)
                throw new ArgumentException("La categoría especificada no existe.");

            return await _bookCategoryRepository.UpdateAsync(bookCategory);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _bookCategoryRepository.DeleteAsync(id);
        }
    }
}
