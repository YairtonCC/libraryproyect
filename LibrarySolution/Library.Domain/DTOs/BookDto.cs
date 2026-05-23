namespace Library.Domain.DTOs
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public int PublishedYear { get; set; }
        public int AuthorId { get; set; }
    }

    public class CreateBookDto
    {
        public string Title { get; set; }
        public string ISBN { get; set; }
        public int PublishedYear { get; set; }
        public int AuthorId { get; set; }
    }
}
