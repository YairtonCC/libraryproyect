using Library.Domain.Enities;
using Library.Domain.Interfaces.Repositories;
using Library.Domain.Interfaces.Services;


namespace LibraryProyect.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;

        public CategoryService(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Category> AddAsync(Category category)
        {
            return await _repository.AddAsync(category);
        }

        public async Task<bool> UpdateAsync(Category category)
        {
            return await _repository.UpdateAsync(category);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}

