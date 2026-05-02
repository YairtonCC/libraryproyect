using Library.Domain.DTOs;

namespace Library.Domain.DTOs
{
    public class AuthorDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
    }

    public class CreateAuthorDto
    {
        public string Name { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
    }
}
