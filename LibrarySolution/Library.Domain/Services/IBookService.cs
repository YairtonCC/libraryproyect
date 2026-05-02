using Library.Domain.Enities;
using Library.Domain.Entities;

namespace Library.Domain.Interfaces.Services
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetAllAsync();
        Task<Book?> GetByIdAsync(int id);
        Task<Book> CreateAsync(Book book);
        Task<bool> UpdateAsync(int id, Book book);
        Task<bool> DeleteAsync(int id);
    }
}
