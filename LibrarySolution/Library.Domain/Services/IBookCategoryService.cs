
namespace Library.Domain.Interfaces.Services
{
    public interface IBookCategoryService
    {
        Task<IEnumerable<BookCategory>> GetAllAsync();
        Task<BookCategory?> GetByIdAsync(int id);
        Task<BookCategory> AddAsync(BookCategory bookCategory);
        Task<bool> UpdateAsync(BookCategory bookCategory);
        Task<bool> DeleteAsync(int id);
    }
}
