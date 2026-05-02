namespace Library.Domain.DTOs
{
    public class BookCategoryDto
    {
        public int BookId { get; set; }
        public int CategoryId { get; set; }
    }

    public class CreateBookCategoryDto
    {
        public int BookId { get; set; }
        public int CategoryId { get; set; }
    }
}
