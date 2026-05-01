using Library.Domain.Enities.Library.Domain.Entities;

namespace Library.Domain.Enities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        // Relación N:M → Un libro puede pertenecer a varias categorías
        public ICollection<BookCategory> BookCategories { get; set; } = new List<BookCategory>();
    }
}
