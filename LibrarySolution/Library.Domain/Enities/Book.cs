using Library.Domain.Enities.Library.Domain.Entities;
using Library.Domain.Entities;


namespace Library.Domain.Enities
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;
        public int PublishedYear { get; set; }

        // Relación con Author
        public int AuthorId { get; set; }
        public Author Author { get; set; }

        // Relación N:M con Category
        public ICollection<BookCategory> BookCategories { get; set; } = new List<BookCategory>();

        // Relación 1:N con Loan
        public ICollection<Loan> Loans { get; set; } = new List<Loan>();
    }
}
