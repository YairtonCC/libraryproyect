using Library.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Domain.Interfaces.Repositories
{
    public interface IAuthorRepository : IGenericRepository<Author>
    {
        Task<IEnumerable<Author>> GetByNameAsync(string name);
        Task<IEnumerable<Author>> GetAuthorsWithBooksAsync();
    }
}
