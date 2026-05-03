using Library.Domain.Enities;


namespace Library.Domain.Entities
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }

        // Relación 1:N → Un autor tiene muchos libros
        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
