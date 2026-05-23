using Library.DataAccess.Context;
using Library.Domain.Entities;
using Library.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.DataAccess.Repositories
{
    public class BookCategoryRepository : GenericRepository<BookCategory>, IBookCategoryRepository
    {
        private readonly LibraryDbContext _context;

        public BookCategoryRepository(LibraryDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BookCategory>> GetByBookIdAsync(int bookId)
        {
            return await _context.BookCategories
                .Where(bc => bc.BookId == bookId)
                .Include(bc => bc.Category)
                .ToListAsync();
        }

        public async Task<IEnumerable<BookCategory>> GetByCategoryIdAsync(int categoryId)
        {
            return await _context.BookCategories
                .Where(bc => bc.CategoryId == categoryId)
                .Include(bc => bc.Book)
                .ToListAsync();
        }
    }
}
