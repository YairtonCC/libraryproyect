using Library.Domain.Enities;
using Library.Domain.Entities;
using Library.Domain.Interfaces.Repositories;
using Library.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryProyect.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _categoryRepository.GetAllAsync();
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            return await _categoryRepository.GetByIdAsync(id);
        }

        public async Task<Category> AddAsync(Category category)
        {
            // Validación: nombre obligatorio
            if (string.IsNullOrWhiteSpace(category.Name))
                throw new ArgumentException("El nombre de la categoría es obligatorio.");

            // Validación: nombre único
            var existing = await _categoryRepository.GetByNameAsync(category.Name);
            if (existing != null)
                throw new ArgumentException("Ya existe una categoría con ese nombre.");

            return await _categoryRepository.AddAsync(category);
        }

        public async Task<bool> UpdateAsync(Category category)
        {
            if (string.IsNullOrWhiteSpace(category.Name))
                throw new ArgumentException("El nombre de la categoría es obligatorio.");

            var existing = await _categoryRepository.GetByNameAsync(category.Name);
            if (existing != null && existing.Id != category.Id)
                throw new ArgumentException("Ya existe otra categoría con ese nombre.");

            return await _categoryRepository.UpdateAsync(category);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _categoryRepository.DeleteAsync(id);
        }
    }
}
