using Library.DataAccess.Context;
using Library.Domain.Enities.Library.Domain.Entities;
using Library.Domain.Entities;
using Library.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Library.DataAccess.Repositories
{
    public class BookCategoryRepository : IBookCategoryRepository
    {
        private readonly LibraryDbContext _context;

        public BookCategoryRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BookCategory>> GetAllAsync()
        {
            return await _context.BookCategories
                .Include(bc => bc.Book)
                .Include(bc => bc.Category)
                .ToListAsync();
        }

        public async Task<BookCategory?> GetByIdsAsync(int bookId, int categoryId)
        {
            return await _context.BookCategories
                .Include(bc => bc.Book)
                .Include(bc => bc.Category)
                .FirstOrDefaultAsync(bc => bc.BookId == bookId && bc.CategoryId == categoryId);
        }

        public async Task AddAsync(BookCategory bookCategory)
        {
            _context.BookCategories.Add(bookCategory);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int bookId, int categoryId)
        {
            var bookCategory = await _context.BookCategories
                .FirstOrDefaultAsync(bc => bc.BookId == bookId && bc.CategoryId == categoryId);

            if (bookCategory != null)
            {
                _context.BookCategories.Remove(bookCategory);
                await _context.SaveChangesAsync();
            }
        }
    }
}
