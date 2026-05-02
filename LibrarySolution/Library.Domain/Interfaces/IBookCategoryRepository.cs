using Library.Domain.Enities.Library.Domain.Entities;
using Library.Domain.Entities;

namespace Library.Domain.Interfaces.Repositories
{
    public interface IBookCategoryRepository
    {
        Task<IEnumerable<BookCategory>> GetAllAsync();
        Task<BookCategory?> GetByIdsAsync(int bookId, int categoryId);
        Task AddAsync(BookCategory bookCategory);
        Task DeleteAsync(int bookId, int categoryId);
    }
}
