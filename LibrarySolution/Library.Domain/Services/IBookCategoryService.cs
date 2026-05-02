using Library.Domain.Enities.Library.Domain.Entities;
using Library.Domain.Entities;

namespace Library.Domain.Interfaces.Services
{
    public interface IBookCategoryService
    {
        Task<IEnumerable<BookCategory>> GetAllAsync();
        Task<BookCategory?> GetByIdsAsync(int bookId, int categoryId);
        Task<BookCategory> CreateAsync(BookCategory bookCategory);
        Task<bool> DeleteAsync(int bookId, int categoryId);
    }
}
