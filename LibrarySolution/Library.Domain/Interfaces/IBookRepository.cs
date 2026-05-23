using Library.Domain.Enities;
using Library.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Domain.Interfaces.Repositories
{
    public interface IBookRepository : IGenericRepository<Book>
    {
        Task<IEnumerable<Book>> GetByAuthorIdAsync(int authorId);
        Task<IEnumerable<Book>> GetByCategoryIdAsync(int categoryId);
        Task<Book?> GetByIsbnAsync(string isbn);
    }
}
