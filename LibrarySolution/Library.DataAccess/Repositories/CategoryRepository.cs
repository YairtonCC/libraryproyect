using Library.DataAccess.Context;
using Library.Domain.Enities;
using Library.Domain.Entities;
using Library.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.DataAccess.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        private readonly LibraryDbContext _context;

        public CategoryRepository(LibraryDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Category?> GetByNameAsync(string name)
        {
            return await _context.Categories
                .FirstOrDefaultAsync(c => c.Name == name);
        }

        public async Task<IEnumerable<Category>> GetCategoriesWithBooksAsync()
        {
            return await _context.Categories
                .Include(c => c.BookCategories)
                .ThenInclude(bc => bc.Book)
                .ToListAsync();
        }
    }
}
