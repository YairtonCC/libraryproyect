using Library.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Domain.Interfaces.Repositories
{
    public interface IBookCategoryRepository : IGenericRepository<BookCategory>
    {
        Task<IEnumerable<BookCategory>> GetByBookIdAsync(int bookId);
        Task<IEnumerable<BookCategory>> GetByCategoryIdAsync(int categoryId);
    }
}
