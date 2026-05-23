using Library.Domain.Entities;
using Library.Domain.Interfaces.Repositories;
using Library.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryProyect.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<IEnumerable<Author>> GetAllAsync()
        {
            return await _authorRepository.GetAllAsync();
        }

        public async Task<Author?> GetByIdAsync(int id)
        {
            return await _authorRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Author>> GetByNameAsync(string name)
        {
            return await _authorRepository.GetByNameAsync(name);
        }

        public async Task<IEnumerable<Author>> GetAuthorsWithBooksAsync()
        {
            return await _authorRepository.GetAuthorsWithBooksAsync();
        }

        public async Task<Author> AddAsync(Author author)
        {
            // Validación: nombre obligatorio
            if (string.IsNullOrWhiteSpace(author.Name))
                throw new ArgumentException("El nombre del autor es obligatorio.");

            // Validación: fecha de nacimiento no futura
            if (author.BirthDate > DateTime.Now)
                throw new ArgumentException("La fecha de nacimiento no puede ser futura.");

            // Validación: evitar duplicados por nombre
            var existing = await _authorRepository.GetByNameAsync(author.Name);
            if (existing != null && existing.Any())
                throw new ArgumentException("Ya existe un autor con ese nombre.");

            return await _authorRepository.AddAsync(author);
        }

        public async Task<bool> UpdateAsync(Author author)
        {
            // Validación: nombre obligatorio
            if (string.IsNullOrWhiteSpace(author.Name))
                throw new ArgumentException("El nombre del autor es obligatorio.");

            // Validación: fecha de nacimiento no futura
            if (author.BirthDate > DateTime.Now)
                throw new ArgumentException("La fecha de nacimiento no puede ser futura.");

            return await _authorRepository.UpdateAsync(author);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _authorRepository.DeleteAsync(id);
        }
    }
}
