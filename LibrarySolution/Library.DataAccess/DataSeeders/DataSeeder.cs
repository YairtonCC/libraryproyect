using Library.DataAccess.Context;
using Library.Domain.Enities;
using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.DataAccess.Seeders
{
    public static class DataSeeder
    {
        public static void Seed(LibraryDbContext context)
        {
            context.Database.Migrate();

            // 🔹 Autores
            if (!context.Authors.Any())
            {
                var authors = new List<Author>
                {
                    new Author { Name = "Gabriel García Márquez", BirthDate = new DateTime(1927, 3, 6) },
                    new Author { Name = "Jorge Isaacs", BirthDate = new DateTime(1837, 4, 1) },
                    new Author { Name = "Rafael Pombo", BirthDate = new DateTime(1833, 11, 7) },
                    new Author { Name = "Álvaro Mutis", BirthDate = new DateTime(1923, 8, 25) },
                    new Author { Name = "Fernando Vallejo", BirthDate = new DateTime(1942, 10, 24) },
                    new Author { Name = "William Ospina", BirthDate = new DateTime(1954, 3, 2) },
                    new Author { Name = "Laura Restrepo", BirthDate = new DateTime(1950, 1, 1) },
                    new Author { Name = "José Asunción Silva", BirthDate = new DateTime(1865, 11, 26) },
                    new Author { Name = "Jaime Jaramillo Escobar", BirthDate = new DateTime(1932, 5, 17) },
                    new Author { Name = "Tomás Carrasquilla", BirthDate = new DateTime(1858, 1, 21) }
                };
                context.Authors.AddRange(authors);
                context.SaveChanges();
            }

            // 🔹 Categorías
            if (!context.Categories.Any())
            {
                var categories = new List<Category>
                {
                    new Category { Name = "Novela" },
                    new Category { Name = "Poesía" },
                    new Category { Name = "Ensayo" },
                    new Category { Name = "Infantil" }
                };
                context.Categories.AddRange(categories);
                context.SaveChanges();
            }

            // 🔹 Libros
            if (!context.Books.Any())
            {
                var books = new List<Book>
                {
                    new Book { Title = "Cien años de soledad", ISBN = "9780307474728", PublishedYear = 1967, AuthorId = 1 },
                    new Book { Title = "María", ISBN = "9789583001234", PublishedYear = 1867, AuthorId = 2 },
                    new Book { Title = "Delirio", ISBN = "9788433971630", PublishedYear = 2004, AuthorId = 7 },
                    new Book { Title = "La vorágine", ISBN = "9789583005676", PublishedYear = 1924, AuthorId = 10 },
                    new Book { Title = "El coronel no tiene quien le escriba", ISBN = "9780307389732", PublishedYear = 1961, AuthorId = 1 },
                    new Book { Title = "Los poemas de Pombo", ISBN = "9789583007892", PublishedYear = 1880, AuthorId = 3 },
                    new Book { Title = "La Virgen de los Sicarios", ISBN = "9789583004563", PublishedYear = 1994, AuthorId = 5 },
                    new Book { Title = "El país de la canela", ISBN = "9789583009872", PublishedYear = 2008, AuthorId = 6 },
                    new Book { Title = "La casa grande", ISBN = "9789583006543", PublishedYear = 1962, AuthorId = 4 },
                    new Book { Title = "Obra poética", ISBN = "9789583003210", PublishedYear = 1896, AuthorId = 8 }
                };
                context.Books.AddRange(books);
                context.SaveChanges();
            }

            // 🔹 Relaciones libro-categoría
            if (!context.BookCategories.Any())
            {
                var relations = new List<BookCategory>
                {
                    new BookCategory { BookId = 1, CategoryId = 1 },
                    new BookCategory { BookId = 2, CategoryId = 1 },
                    new BookCategory { BookId = 3, CategoryId = 1 },
                    new BookCategory { BookId = 6, CategoryId = 2 },
                    new BookCategory { BookId = 7, CategoryId = 1 },
                    new BookCategory { BookId = 8, CategoryId = 3 },
                    new BookCategory { BookId = 9, CategoryId = 1 },
                    new BookCategory { BookId = 10, CategoryId = 2 }
                };
                context.BookCategories.AddRange(relations);
                context.SaveChanges();
            }

            // 🔹 Miembros ficticios
            if (!context.Members.Any())
            {
                var members = new List<Member>
                {
                    new Member { Name = "Juan Pérez", Email = "juan.perez@example.com", JoinDate = DateTime.Now.AddYears(-1) },
                    new Member { Name = "María Gómez", Email = "maria.gomez@example.com", JoinDate = DateTime.Now.AddMonths(-6) },
                    new Member { Name = "Carlos Rodríguez", Email = "carlos.rodriguez@example.com", JoinDate = DateTime.Now.AddMonths(-3) }
                };
                context.Members.AddRange(members);
                context.SaveChanges();
            }

            // 🔹 Préstamos iniciales
            if (!context.Loans.Any())
            {
                var loans = new List<Loan>
                {
                    new Loan { BookId = 1, MemberId = 1, LoanDate = DateTime.Now.AddDays(-10), ReturnDate = DateTime.Now.AddDays(5), Status = LoanStatus.Active },
                    new Loan { BookId = 2, MemberId = 2, LoanDate = DateTime.Now.AddDays(-20), ReturnDate = DateTime.Now.AddDays(-5), Status = LoanStatus.Overdue },
                    new Loan { BookId = 3, MemberId = 3, LoanDate = DateTime.Now.AddDays(-15), ReturnDate = DateTime.Now.AddDays(-1), Status = LoanStatus.Returned }
                };
                context.Loans.AddRange(loans);
                context.SaveChanges();
            }
        }
    }
}

