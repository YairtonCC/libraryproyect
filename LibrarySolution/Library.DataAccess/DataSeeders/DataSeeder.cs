using Library.DataAccess.Context;
using Library.Domain.Enities;
using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.DataAccess.Seeders
{
    public static class DataSeeder
    {
        public static async Task SeedAsync(LibraryDbContext context)
        {
            // Solo ejecutar si no hay autores
            if (await context.Authors.AnyAsync()) return;

            // 1. AUTORES COLOMBIANOS
            var authors = new List<Author>
            {
                new() { Name = "Gabriel García Márquez" },
                new() { Name = "Rafael Pombo" },
                new() { Name = "Jaime Jaramillo Escobar" },
                new() { Name = "Álvaro Mutis" },
                new() { Name = "José Asunción Silva" },
                new() { Name = "Jorge Isaacs" },
                new() { Name = "Fernando Vallejo" },
                new() { Name = "William Ospina" },
                new() { Name = "Laura Restrepo" },
                new() { Name = "Piedad Bonnett" }
            };

            // 2. CATEGORÍAS
            var categories = new List<Category>
            {
                new() { Name = "Novela" },
                new() { Name = "Poesía" },
                new() { Name = "Ensayo" },
                new() { Name = "Infantil" }
            };

            // 3. LIBROS
            var books = new List<Book>
            {
                new() { Title = "Cien años de soledad", ISBN = "9780307474728", PublishedYear = 1967, Author = authors[0] },
                new() { Title = "Rin Rin Renacuajo", ISBN = "9789583000001", PublishedYear = 1855, Author = authors[1] },
                new() { Title = "Los poemas de nada", ISBN = "9789583000002", PublishedYear = 1960, Author = authors[2] },
                new() { Title = "La mansión de Araucaíma", ISBN = "9789583000003", PublishedYear = 1973, Author = authors[3] },
                new() { Title = "Nocturno", ISBN = "9789583000004", PublishedYear = 1894, Author = authors[4] },
                new() { Title = "María", ISBN = "9789583000005", PublishedYear = 1867, Author = authors[5] },
                new() { Title = "La virgen de los sicarios", ISBN = "9789583000006", PublishedYear = 1994, Author = authors[6] },
                new() { Title = "Ursúa", ISBN = "9789583000007", PublishedYear = 2005, Author = authors[7] },
                new() { Title = "Delirio", ISBN = "9789583000008", PublishedYear = 2004, Author = authors[8] },
                new() { Title = "Lo que no tiene nombre", ISBN = "9789583000009", PublishedYear = 2013, Author = authors[9] }
            };

            // 4. RELACIONES LIBRO-CATEGORÍA
            var bookCategories = new List<BookCategory>
            {
                new() { Book = books[0], Category = categories[0] },
                new() { Book = books[1], Category = categories[3] },
                new() { Book = books[2], Category = categories[1] },
                new() { Book = books[3], Category = categories[0] },
                new() { Book = books[4], Category = categories[1] },
                new() { Book = books[5], Category = categories[0] },
                new() { Book = books[6], Category = categories[0] },
                new() { Book = books[7], Category = categories[0] },
                new() { Book = books[8], Category = categories[0] },
                new() { Book = books[9], Category = categories[1] }
            };

            // 5. MIEMBROS FICTICIOS
            var members = new List<Member>
            {
                new() { Name = "Juan Pérez", Email = "juan.perez@example.com" },
                new() { Name = "María Gómez", Email = "maria.gomez@example.com" },
                new() { Name = "Carlos Rodríguez", Email = "carlos.rodriguez@example.com" }
            };

            // 6. PRÉSTAMOS DE EJEMPLO
            var loans = new List<Loan>
            {
                new() { Book = books[0], Member = members[0], LoanDate = DateTime.Now.AddDays(-10), ReturnDate = DateTime.Now.AddDays(10) },
                new() { Book = books[5], Member = members[1], LoanDate = DateTime.Now.AddDays(-5), ReturnDate = DateTime.Now.AddDays(15) },
                new() { Book = books[8], Member = members[2], LoanDate = DateTime.Now, ReturnDate = DateTime.Now.AddDays(20) }
            };

            // Guardar en BD
            await context.Authors.AddRangeAsync(authors);
            await context.Categories.AddRangeAsync(categories);
            await context.Books.AddRangeAsync(books);
            await context.BookCategories.AddRangeAsync(bookCategories);
            await context.Members.AddRangeAsync(members);
            await context.Loans.AddRangeAsync(loans);

            await context.SaveChangesAsync();
        }
    }
}
