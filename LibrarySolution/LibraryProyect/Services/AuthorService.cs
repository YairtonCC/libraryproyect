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
            if (id <= 0)
                throw new ArgumentException("El ID debe ser mayor que cero.");

            return await _authorRepository.GetByIdAsync(id);
        }

        public async Task<Author> AddAsync(Author author)
        {
            if (string.IsNullOrWhiteSpace(author.Name))
                throw new ArgumentException("El nombre del autor es obligatorio.");

            if (author.BirthDate == default)
                throw new ArgumentException("La fecha de nacimiento es obligatoria.");

            // Validar duplicados
            var existing = await _authorRepository.GetAllAsync();
            foreach (var a in existing)
            {
                if (a.Name.Equals(author.Name, StringComparison.OrdinalIgnoreCase))
                    throw new ArgumentException("Ya existe un autor con ese nombre.");
            }

            return await _authorRepository.AddAsync(author);
        }

        public async Task<bool> UpdateAsync(Author author)
        {
            if (author.Id <= 0)
                throw new ArgumentException("El ID del autor es inválido.");

            if (string.IsNullOrWhiteSpace(author.Name))
                throw new ArgumentException("El nombre del autor es obligatorio.");

            return await _authorRepository.UpdateAsync(author);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser mayor que cero.");

            return await _authorRepository.DeleteAsync(id);
        }
    }
}
