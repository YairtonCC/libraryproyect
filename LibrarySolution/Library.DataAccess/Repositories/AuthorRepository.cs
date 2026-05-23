using Library.DataAccess.Context;
using Library.Domain.Entities;
using Library.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.DataAccess.Repositories
{
    public class AuthorRepository : GenericRepository<Author>, IAuthorRepository
    {
        private readonly LibraryDbContext _context;

        public AuthorRepository(LibraryDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Author>> GetByNameAsync(string name)
        {
            return await _context.Authors
                .Where(a => a.Name.Contains(name))
                .ToListAsync();
        }

        public async Task<IEnumerable<Author>> GetAuthorsWithBooksAsync()
        {
            return await _context.Authors
                .Include(a => a.Books)
                .ToListAsync();
        }
    }
}
