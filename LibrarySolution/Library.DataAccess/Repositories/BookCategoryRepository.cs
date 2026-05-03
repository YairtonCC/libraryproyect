using Library.DataAccess.Context;
using Library.Domain.Enities.Library.Domain.Entities;
using Library.Domain.Entities;
using Library.Domain.Interfaces.Repositories;

namespace Library.DataAccess.Repositories
{
    public class BookCategoryRepository : GenericRepository<BookCategory>, IBookCategoryRepository
    {
        public BookCategoryRepository(LibraryDbContext context) : base(context)
        {
        }
    }
}
